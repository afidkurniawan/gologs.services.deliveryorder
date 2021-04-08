// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System;
using System.Linq;
using GoLogs.Services.DeliveryOrder.Api.Application.Internals;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Nirbito.Framework.PostgresClient;
using Nirbito.Framework.PostgresClient.DependencyInjectionExtensions;
using Nirbito.Framework.PostgresClient.ManagedColumns;
using Npgsql;
using Serilog;
using ThrowawayDb.Postgres;

namespace GoLogs.Services.DeliveryOrder.Api.FunctionalTests
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var pgContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DOOrderContext));
                services.Remove(pgContextDescriptor);

                var serviceProvider = services.BuildServiceProvider();
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();

                using var scope = serviceProvider.CreateScope();

                var logger = scope.ServiceProvider.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();
                logger.LogDebug("Configuring test host...");

                logger.LogDebug("Creating database...");
                var throwawayDb = ThrowawayDatabase.Create(configuration.GetConnectionString("DO_Order"));
                logger.LogDebug($"Database created: {throwawayDb.Name}");

                var evolve = new Evolve.Evolve(
                    new NpgsqlConnection(throwawayDb.ConnectionString),
                    message => logger.LogDebug($"Evolve: {message}"))
                {
                    Locations = new[] { "database/migrations", "database/datasets" }
                };
                evolve.Migrate();

                services
                    .AddPgContext<DOOrderContext>(options => options
                        .UseConnectionString(throwawayDb.ConnectionString)
                        .UseSoftDeleteColumn(new SoftDeleteColumn<int>(
                            "rowstatus", delete => delete ? 1 : 0))
                        .UseNamingConvention(NamingConvention.SnakeCase));

                var massTransitDescriptors = services.Where(d =>
                    !String.IsNullOrEmpty(d?.ServiceType.Namespace) &&
                    d.ServiceType.Namespace.StartsWith(nameof(MassTransit), StringComparison.InvariantCulture))
                    .ToArray();
                foreach (var descriptor in massTransitDescriptors)
                {
                    services.Remove(descriptor);
                }

                services.AddMassTransit(c =>
                {
                    c.AddBus(provider =>
                    {
                        var control = Bus.Factory.CreateUsingInMemory(cfg =>
                        {
                            cfg.ConfigureEndpoints(provider);
                        });
                        control.Start();
                        return control;
                    });
                });

                logger.LogDebug("Test host configured.");
            });

            builder
                .UseSerilog((context, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .Enrich.FromLogContext());
        }
    }
}
