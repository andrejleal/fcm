namespace Framework.Application.Contracts
{
    public class ChildEntityDTO<TIdType, TDTO> : DTO
        where TDTO : DTO
    {
        public TIdType ParentId
        {
            get;
            set;
        }

        public TDTO Entity
        {
            get;
            set;
        }
    }
}
