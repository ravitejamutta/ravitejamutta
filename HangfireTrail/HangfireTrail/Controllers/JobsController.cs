using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HangfireTrail.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly ILogger<JobsController> _logger;

        public JobsController(ILogger<JobsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Start()
        {
            return Ok("Started");
        }

        [HttpPost]
        public IActionResult AddMinJob()
        {
            // each 5 sec expression added
            RecurringJob.AddOrUpdate(() => CallReminder(), Cron.Minutely);
            return Ok("Added");
        }

        [HttpPost]
        [Route("schedule")]
        public IActionResult ScheduleJob([FromBody]object startTime)
        {
            var obj = JObject.Parse(startTime.ToString());
            DateTime dt = Convert.ToDateTime(obj.GetValue("startTime").ToString());
            BackgroundJob.Schedule(() => CallReminder(), dt);
            return Ok("Added");
        }

        public async Task CallReminder()
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://reminderstest.azurewebsites.net/reminders"),
                Version = HttpVersion.Version10,
                Content = new StringContent("", Encoding.UTF8, "application/json")
            };
            using (var client = new HttpClient())
            {
               await client.SendAsync(httpRequestMessage);
            }
        }
    }
}
