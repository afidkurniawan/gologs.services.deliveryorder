using System;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using FluentValidation.AspNetCore;
using GoLogs.Framework.Core.Options;
using GoLogs.Framework.Mvc;
using MassTransit;
using MassTransit.RabbitMqTransport;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace GoLogs.Services.DeliveryOrder.Api
{
    public class Startup
    {
        private AssemblyName AssemblyName { get; }

        // ReSharper disable once MemberCanBePrivate.Global
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            AssemblyName = Assembly.GetEntryAssembly()?.GetName();
            Configuration = configuration;
        }

        #region RabbitMq
        
        private ServiceOptions _rabbitMqOptions;
        
        private X509Certificate CertificateSelectionCallback(
            object sender, string targetHost, X509CertificateCollection localCertificates,
            X509Certificate remoteCertificate, string[] acceptableIssuers)
        {
            var serverCertificate = localCertificates.OfType<X509Certificate2>()
                .FirstOrDefault(cert => cert.Thumbprint?.ToLower() == _rabbitMqOptions.SslThumbprint.ToLower());

            return serverCertificate ?? throw new Exception("Wrong certificate");
        }

        private void ConfigureRabbitMq(IBusRegistrationContext context, IRabbitMqBusFactoryConfigurator rabbitMqCfg)
        {
            _rabbitMqOptions = Configuration.GetSection(ServiceDependenciesOptions.SERVICE_DEPENDENCIES)
                .Get<ServiceOptions[]>()
                .First(svc => svc.Name.Equals("RabbitMQ"));

            X509Certificate2 x509Certificate2 = null;

            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);

            try
            {
                var certificatesInStore = store.Certificates;

                x509Certificate2 = certificatesInStore.OfType<X509Certificate2>()
                    .FirstOrDefault(cert => cert.Thumbprint?.ToLower() == _rabbitMqOptions.SslThumbprint?.ToLower());
            }
            finally
            {
                store.Close();
            }

            rabbitMqCfg.Host(_rabbitMqOptions.Host, _rabbitMqOptions.VirtualHost, h =>
            {
                h.Username(_rabbitMqOptions.Username);
                h.Password(_rabbitMqOptions.Password);

                if (_rabbitMqOptions.UseSsl)
                {
                    h.UseSsl(ssl =>
                    {
                        ssl.ServerName = Dns.GetHostName();
                        ssl.AllowPolicyErrors(SslPolicyErrors.RemoteCertificateNameMismatch);
                        ssl.Certificate = x509Certificate2;
                        ssl.Protocol = SslProtocols.Tls12;
                        ssl.CertificateSelectionCallback = CertificateSelectionCallback;
                    });
                }
            });

            rabbitMqCfg.ConfigureEndpoints(context);
        }
        
        #endregion
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddFluentValidation();

            services.AddMediatR(typeof(Startup));

            services.AddMassTransit(mt =>
            {
                mt.UsingRabbitMq(ConfigureRabbitMq);
            });

            services
                .AddScoped<IProblemCollector, ProblemCollector>();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = AssemblyName.Name,
                        Version = "v1"
                    }
                );

                var filePath = System.IO.Path.Combine(
                    AppContext.BaseDirectory,
                    String.Concat(AssemblyName.Name, ".xml"));
                c.IncludeXmlComments(filePath);
                c.AddFluentValidationRules();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint(
                        "swagger/v1/swagger.json",
                        String.Concat(AssemblyName.Name, " v1"));
                    c.RoutePrefix = String.Empty;
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}