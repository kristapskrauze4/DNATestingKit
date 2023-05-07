using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using DatabaseAccess.Data;
using DatabaseAccess.Models;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NSubstitute;
using Xunit;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using DotnetDocsShow.Tests.Integration;
using Castle.Core.Resource;
using System.Net.Http.Json;
using DNATestingKit;
using System.Text;
using DNATestingKit.Models;

namespace DnaTestingKit.Tests
{
    public class EndpointTests
    {
        private readonly IOrdersData _ordersData =
            Substitute.For<IOrdersData>();

        [Fact]
        public async Task GetOrderByCustomerId_ReturnCustomer_WhenCustomerExists()
        {
            var orderModel = new List<OrderModel>();
            OrderModel order = new()
            {
                CustomerId = 9999998,
                Amount = 2,
                DeliveryDate = DateTime.Parse("05.05.2023"),
                Sum = 197.98
            };
            orderModel.Add(order);

            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            var response = await httpClient.GetAsync($"/orders/{9999998}");
            var responseText = await response.Content.ReadAsStringAsync();
            var orderResult = JsonSerializer.Deserialize<List<OrderModel>>(responseText, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            orderResult.Should().BeEquivalentTo(orderModel);
        }

        [Fact]
        public async Task GetOrderById_ReturnNotFound_WhenOrderDoesNotExists()
        {
            using var app = new AppFactory();

            var httpClient = app.CreateClient();

            var response = await httpClient.GetAsync($"/orders/{-2}");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task CreateOrderAsync_WithItemToCreate_ReturnsCreatedItem()
        {
            InsertOrderModel order = new()
            {
                CustomerId = -5,
                Amount = 2,
                DeliveryDate = DateTime.Now.AddDays(1)
            };

            var content = JsonSerializer.Serialize(order).ToString();
            using var app = new AppFactory();

            var httpClient = app.CreateClient();

            var result = await httpClient.PostAsync($"/orders", new StringContent(content, Encoding.UTF8, "application/json"));

            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task CreateOrderAsync_WithoutItemToCreate_ReturnsCanNotCreate()
        {
            InsertOrderModel order = new();

            var content = JsonSerializer.Serialize(order).ToString();
            using var app = new AppFactory();

            var httpClient = app.CreateClient();

            var result = await httpClient.PostAsync($"/orders", new StringContent(content, Encoding.UTF8, "application/json"));

            result.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }


        [Theory]
        [InlineData(999991, -1, "9999.01.01")]
        [InlineData(999991, 1000, "9999.01.01")]
        [InlineData(999991, 3, "2022.01.01")]
        public async Task CreateOrderAsync_InvalidValues_ReturnCanNotCreate(int customerId, int amount, DateTime deliveryDate)
        {
            InsertOrderModel order = new();

            var content = JsonSerializer.Serialize(order).ToString();
            using var app = new AppFactory();

            var httpClient = app.CreateClient();

            var result = await httpClient.PostAsync($"/orders", new StringContent(content, Encoding.UTF8, "application/json"));

            result.StatusCode.Should().Be(HttpStatusCode.InternalServerError);

        }
    }
}