using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using FCM.API.REST.Contracts;
using System.Collections.Generic;

namespace FCM.Client.App.Controllers
{
    [Route("api/[controller]")]
    public class ExternalSystemsController : Controller
    {
        private IConfigurationRoot root;

        public ExternalSystemsController()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json", optional: false);
            this.root = builder.Build();
        }

        private string GetHeaderValue(string headerName)
        {
            var value = Request.Headers[headerName];

            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            return value;

        }

        [HttpGet("{externalSystemName}/components")]
        /// <summary>
        /// Returns current component configurations for external system idenfied by externalSystemName parameter
        /// </summary>
        /// <returns></returns>
        public IActionResult GetComponents(string externalSystemName)
        {
            var systemConfigJson = System.IO.File.Exists(externalSystemName) ? System.IO.File.ReadAllText(externalSystemName) : null;

            var resultModel = systemConfigJson == null ? null : JsonConvert.DeserializeObject<RestResponseWithData<List<ComponentModel>>>(systemConfigJson);

            return new JsonResult(resultModel);
        }


        [HttpPost("{name}/components/refresh")]
        /// <summary>
        /// Force the component configuration refresh
        /// </summary>
        /// <returns></returns>
        public IActionResult Post(string name)
        {
            var requestNotificationToken = this.GetHeaderValue("Authorization");

            if (string.IsNullOrWhiteSpace(requestNotificationToken))
            {
                return Unauthorized();
            }

            if (requestNotificationToken.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                requestNotificationToken = requestNotificationToken.Substring("Bearer ".Length).Trim();
            }

            var externalSystemConfigSection = this.root.GetSection("externalSystems").GetSection(name);

            var systemNotificationToken = externalSystemConfigSection.GetValue<string>("notificationToken");

            if(systemNotificationToken != requestNotificationToken)
            {
                return Unauthorized();
            }

            var fcmAppUrl = this.root.GetValue<string>("fcmAppUrl");
            var systemToken = externalSystemConfigSection.GetValue<string>("fcmToken");

            this.RefreshComponentsForExternalSystem(name, fcmAppUrl, systemToken);
            return Ok();
        }

        private async void RefreshComponentsForExternalSystem(string name, string fcmAppUrl, string systemToken)
        {
            await this.GetComponentsFromFCM(name, fcmAppUrl, systemToken);
        }

        private async Task<int> GetComponentsFromFCM(string name, string fcmAppUrl, string systemToken)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, fcmAppUrl);
                    if (!string.IsNullOrWhiteSpace(systemToken))
                    {
                        request.Headers.Add("Authorization", $"Bearer {systemToken}");
                    }

                    using (HttpResponseMessage response = await client.SendAsync(request))
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            Console.WriteLine($"Error connecting to {fcmAppUrl}");
                        }
                        else
                        {
                            System.IO.File.WriteAllText(name, response.Content.ReadAsStringAsync().Result);
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"Error connecting to {fcmAppUrl}");
            }

            return 1;
        }
    }
}
