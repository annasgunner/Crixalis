using grpcCrixalis.Data;
using Pantheon.Bases.BaseBlazorServer.Repositories.BaseRepoEtm.RepoT0Jabatan;
using Pantheon.Bases.BaseBlazorServer.Repositories.RepoGeneric;
using Pantheon.Bases.BaseBlazorShared.BaseEtms;

namespace Pantheon.Repository
{
    public class RepoJabatan : pthRepoGeneric<DbT0Jabatan>, IRepoJabatan
    {
        public CrixalisDbContext? Context => Db as CrixalisDbContext;
        public RepoJabatan(CrixalisDbContext db) : base(db)
        {

        }

    }
}
