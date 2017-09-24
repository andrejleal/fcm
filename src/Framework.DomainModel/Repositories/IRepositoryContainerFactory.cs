namespace Framework.DomainModel.Repositories
{
    public interface IRepositoryContainerFactory<T>
        where T : IRepositoryContainer
    {
        T GetInstance(IUnitOfWork unitOfWork);
    }
}
