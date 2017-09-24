using FCM.Application.Operations;

namespace FCM.Application
{
    public interface IFCMApplication
    {
        AddOrUpdateComponentOperation AddOrUpdateComponentOperation { get; }
        AddOrUpdateExternalSystemOperation AddOrUpdateExternalSystemOperation { get; }
        GetAllComponentOperation GetAllComponentOperation { get; }
        GetComponentsForExternalSystemOperation GetComponentsForExternalSystemOperation { get; }
        GetAllExternalSystemOperation GetAllExternalSystemOperation { get; }
        RemoveComponentOperation RemoveComponentOperation { get; }
        RemoveExternalSystemOperation RemoveExternalSystemOperation { get; }
    }
}
