
using FCM.DomainModel.Entities;
using FCM.DomainModel.Repositories;
using Framework.Repositories.EF;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FCM.Repositories.EF
{
    public class EFComponentRepository : EFAggregateRootRepository<Component>, IComponentRepository
    {
        public EFComponentRepository(FCMContext context) : base(context)
        {
            this.context = context;
        }

        public void AddProperty(string componentName, ComponentProperty property)
        {
            var entity = this.GetByName(componentName);
            entity.Properties.Append(property);
            this.Update(entity);
        }

        public IEnumerable<Component> GetAllWithProperties()
        {
            return this.context.Set<Component>().Include(o => o.Properties);
        }

        public Component GetByName(string name)
        {
            return this.context.Set<Component>().Include(o => o.Properties).FirstOrDefault(o => o.Name == name);
        }

        public void RemoveProperties(string componentName)
        {
            var entity = this.GetByName(componentName);
            entity.Properties = new List<ComponentProperty>();
            this.Update(entity);
        }

        public void RemoveProperty(string componentName, string propertyName)
        {
            var entity = this.context.Set<Component>().Include(o => o.Properties).FirstOrDefault(o => o.Name == componentName);
            var itemToRemove = entity.Properties.FirstOrDefault(p => p.Name == componentName);
            if (itemToRemove != null)
            {
                var propertyList = entity.Properties.ToList();
                propertyList.Remove(itemToRemove);
                entity.Properties = propertyList;
            }
            this.Update(entity);
        }
    }
}
