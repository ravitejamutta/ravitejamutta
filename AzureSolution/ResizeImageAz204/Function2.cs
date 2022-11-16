using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace ResizeImageAz204
{
    public class Function2
    {
        [FunctionName("Function2")]
        public void Run([BlobTrigger("images/{name}", Connection = "StorageConnectionString")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
