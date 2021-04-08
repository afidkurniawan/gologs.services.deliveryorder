// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using GoLogs.Services.DeliveryOrder.Api.Commands;
using GoLogs.Services.DeliveryOrder.Api.Models;
using Xunit;

namespace GoLogs.Services.DeliveryOrder.Api.FunctionalTests
{
    public class EndToEndTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private const string EndpointUri = "/DOOrders";
        private readonly HttpClient _client;

        public EndToEndTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAsync_ReturnsAll()
        {
            const int DOOrdersCount = 3;

            var response = await _client.GetAsync(EndpointUri);

            response.EnsureSuccessStatusCode();
            var doOrders = await response.Content.ReadFromJsonAsync<IList<DOOrder>>();
            Assert.NotNull(doOrders);
            Assert.Equal(DOOrdersCount, doOrders.Count);
        }

        [Fact]
        public async Task CreateAsync_GetAsync_ReturnsCreated()
        {
            const int CargoOwnerId = 3;
            var newDOOrderCommand = new CreateOrderCommand { CargoOwnerId = CargoOwnerId };
            var postResponse = await _client.PostAsync(
                EndpointUri,
                new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(newDOOrderCommand), Encoding.UTF8,
                    "application/json"));

            postResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

            var getResponse = await _client.GetAsync(postResponse.Headers.Location);

            getResponse.EnsureSuccessStatusCode();
            var doOrder = await getResponse.Content.ReadFromJsonAsync<DOOrder>();
            Assert.NotNull(doOrder);
            Assert.Equal(newDOOrderCommand.CargoOwnerId, doOrder.CargoOwnerId);
        }
    }
}
