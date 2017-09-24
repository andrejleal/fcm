using System;
using System.Collections.Generic;
using FCM.DomainModel.Entities;
using System.Threading.Tasks;
using System.Net.Http;

namespace FCM.Application
{
    public class NotificationService : INotificationService
    {
        public void NotifySystemsThatComponentChanged(IEnumerable<ExternalSystem> externalSystemsToNofity, Component updatedComponent)
        {
            this.InternalNotifySystemsThatComponentChanged(externalSystemsToNofity);
        }


        private async void InternalNotifySystemsThatComponentChanged(IEnumerable<ExternalSystem> systemsToNotify)
        {
            await this.NotifySystemsThatComponentsChangedMethod(systemsToNotify);
        }

        private async Task<int> NotifySystemsThatComponentsChangedMethod(IEnumerable<ExternalSystem> systemsToNotify)
        {
            foreach (var system in systemsToNotify)
            {
                if (string.IsNullOrWhiteSpace(system.NotificationURL))
                {
                    Console.WriteLine($"NotificationURL not defined for system {system.Name}");
                }
                else
                {
                    try
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, system.NotificationURL);
                            if (!string.IsNullOrWhiteSpace(system.NotificationToken))
                            {
                                request.Headers.Add("Authorization", system.NotificationToken);
                            }

                            using (HttpResponseMessage response = await client.SendAsync(request))
                            {
                                if (!response.IsSuccessStatusCode)
                                {
                                    Console.WriteLine($"Error connecting to {system.Name}");
                                }
                                else
                                {
                                    Console.WriteLine($"{system.Name} has been notified.");
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"Error connecting to {system.Name}");
                    }
                }
            }
            return 1;
        }
    }
}
