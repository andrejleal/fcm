using FCM.DomainModel.Entities;
using Framework.DomainModel.Repositories;
using System.Collections.Generic;

namespace FCM.DomainModel.Repositories
{
    public interface IComponentRepository : IAggregateRootRepository<Component>
    {
        Component GetByName(string name);
        IEnumerable<Component> GetAllWithProperties();

        void AddProperty(string componentName, ComponentProperty property);
        void RemoveProperties(string componentName);
        void RemoveProperty(string componentName, string propertyName);
    }
}
