using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Pages.Basket;

namespace Microsoft.eShopWeb.Web.Services
{
    public class OrderSenderService : IOrderSenderService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<OrderSenderService> _logger;
        private HttpClient _httpClient;

        public OrderSenderService(IHttpClientFactory httpClientFactory, ILogger<OrderSenderService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _httpClient = _httpClientFactory.CreateClient("OrderItemsReserverClient");
        }

        public async Task SendToWarehouse(IEnumerable<BasketItemViewModel> items)
        {
            
            var orderItems =
                items.Select(x => new OrderItem() { CatalogItemId = x.CatalogItemId, Quantity = x.Quantity })
                    .ToArray();
            var str = JsonSerializer.Serialize(orderItems);
            _logger.LogWarning($"Sending data: {str}");
            await _httpClient.PostAsync(string.Empty, new StringContent(str, Encoding.UTF8,  MediaTypeNames.Application.Json));
        }
    }
}
