using FCM.DomainModel.Entities;
using System.Collections.Generic;

namespace FCM.Application
{
    public interface INotificationService
    {
        void NotifySystemsThatComponentChanged(IEnumerable<ExternalSystem> externalSystemsToNofity, Component updatedComponent);
    }
}
