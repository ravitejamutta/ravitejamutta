using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace HangfireTrail
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            Hangfire.Azure.CosmosDbStorageOptions options = new Hangfire.Azure.CosmosDbStorageOptions
            {
                ExpirationCheckInterval = TimeSpan.FromMinutes(2),
                CountersAggregateInterval = TimeSpan.FromMinutes(2),
                QueuePollInterval = TimeSpan.FromSeconds(5)
            };
            services.AddControllers();
            
            services.AddHangfire(configuration => configuration
               .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
               .UseSimpleAssemblyNameTypeSerializer()
               .UseRecommendedSerializerSettings()
               .UseAzureCosmosDbStorage("https://platoondb.documents.azure.com:443/",
                      "BCAKjBR6sEe0XplQBe4WUXfpieJZH2iyymErPUSV1TT5XuIL7dL8wXWwEwrAsXNTMXAQhtXMWFxH5sJeaqO7iQ==", "SchedulerDB", "Schedules", storageOptions: options));
            var count = Environment.ProcessorCount;
            services.AddHangfireServer(options =>
            {
                options.WorkerCount = 25;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            GlobalConfiguration.Configuration.UseAzureCosmosDbStorage("https://platoondb.documents.azure.com:443/",
                      "BCAKjBR6sEe0XplQBe4WUXfpieJZH2iyymErPUSV1TT5XuIL7dL8wXWwEwrAsXNTMXAQhtXMWFxH5sJeaqO7iQ==", "SchedulerDB", "Schedules");

            Hangfire.Azure.CosmosDbStorage storage = new Hangfire.Azure.CosmosDbStorage("https://platoondb.documents.azure.com:443/",
                      "BCAKjBR6sEe0XplQBe4WUXfpieJZH2iyymErPUSV1TT5XuIL7dL8wXWwEwrAsXNTMXAQhtXMWFxH5sJeaqO7iQ==", "SchedulerDB", "Schedules");
            GlobalConfiguration.Configuration.UseStorage(storage);

            app.UseHangfireDashboard();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });
        }
    }
}
