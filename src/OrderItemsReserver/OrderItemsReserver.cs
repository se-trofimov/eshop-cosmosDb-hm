using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;


namespace OrderItemsReserver
{
    public static class OrderItemsReserver
    {
        [FunctionName("OrderItemsReserver")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post")] OrderItem[] request,
            [Blob("orders/{DateTime}.json", FileAccess.Write, Connection = "AzureWebJobsStorage")] Stream outputBlob,
            ILogger log)
        {
            log.LogInformation($"C# HTTP trigger function processed a request. Data Received: {JsonSerializer.Serialize(request)}");
            await JsonSerializer.SerializeAsync(outputBlob, request);
            return new OkObjectResult("Request processed");
        }
    }
}
