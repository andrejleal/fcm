using FCM.Application.Contracts;
using FCM.DomainModel.Entities;
using System.Collections.Generic;
using System;

namespace FCM.Application
{
    public class FCMMapper
    {
        public Component MapComponentDTOToComponent(ComponentDTO source)
        {
            var result = new Component()
            {
                Owner = source.Owner,
                Name = source.Name,
            };

            if (source.Properties != null)
            {
                var properties = new List<ComponentProperty>();

                foreach (var propertyDTO in source.Properties)
                {
                    var property = this.MapComponentPropertyDTOToComponentProperty(propertyDTO);
                    property.Parent = result;
                    properties.Add(property);
                }
                result.Properties = properties;
            }

            return result;
        }

        public ComponentProperty MapComponentPropertyDTOToComponentProperty(ComponentPropertyDTO propertyDTO)
        {
            return new ComponentProperty()
            {
                Name = propertyDTO.Name,
                Value = propertyDTO.Value,
                Owner = propertyDTO.Owner
            };
        }


        public IEnumerable<ComponentDTO> MapComponentToComponentDTO(IEnumerable<Component> source)
        {
            var list = new List<ComponentDTO>();
            foreach(var component in source)
            {
                list.Add(this.MapComponentToComponentDTO(component));
            }
            return list;
        }

        public ComponentDTO MapComponentToComponentDTO(Component source)
        {
            var result = new ComponentDTO()
            {
                Owner = source.Owner,
                Name = source.Name,
            };

            if (source.Properties != null)
            {
                var properties = new List<ComponentPropertyDTO>();

                foreach (var propertyDTO in source.Properties)
                {
                    var property = this.MapComponentPropertyToComponentPropertyDTO(propertyDTO);
                    properties.Add(property);
                }
                result.Properties = properties;
            }

            return result;
        }

        public ComponentPropertyDTO MapComponentPropertyToComponentPropertyDTO(ComponentProperty propertyDTO)
        {
            return new ComponentPropertyDTO()
            {
                Name = propertyDTO.Name,
                Value = propertyDTO.Value,
                Owner = propertyDTO.Owner
            };
        }

    }
}
