namespace DropStorage.WebApi.DataModel.Core.Abstractions
{
    public interface IUnitOfWork
    {
        bool Commit();

        Task<bool> CommitAsync();
    }
}
