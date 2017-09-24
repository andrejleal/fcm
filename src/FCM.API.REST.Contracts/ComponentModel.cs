using System.Collections.Generic;

namespace FCM.API.REST.Contracts
{
    public class ComponentModel
    {
        public string Name
        {
            get;
            set;
        }

        public List<ComponentPropertyModel> Properties
        {
            get;
            set;
        }

        public string Owner
        {
            get;
            set;
        }
    }
}
