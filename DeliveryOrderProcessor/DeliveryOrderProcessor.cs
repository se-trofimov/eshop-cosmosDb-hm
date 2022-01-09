using System.Threading.Tasks;
using DeliveryOrderProcessor.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace DeliveryOrderProcessor
{
    public static class DeliveryOrderProcessor
    {
        [FunctionName("DeliveryOrderProcessor")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post")] OrderDetails request,
            [CosmosDB(
                databaseName: "EShopOnWeb",
                collectionName: "Delivery",
                ConnectionStringSetting = "CosmosDBConnection",
                PartitionKey = "{OrderId}")] IAsyncCollector<OrderDetails> item,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            await item.AddAsync(request);

            return new OkObjectResult("Message processed");
        }
    }
}
