global using Pantheon.Repository;
using Pantheon.Bases.BaseBlazorServer.DataAccess;

namespace grpcCrixalis.Data
{
    public interface IUnitOfWork : pthIUnitOfWork
    {
        IRepoForm RepoForm { get; }
    }
}
