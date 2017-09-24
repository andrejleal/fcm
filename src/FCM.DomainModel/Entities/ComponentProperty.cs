using Framework.DomainModel.Entities;
namespace FCM.DomainModel.Entities
{
    public class ComponentProperty : ChildEntity<Component>
    {
        public string Name
        {
            get;
            set;
        }
        public string Value
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
