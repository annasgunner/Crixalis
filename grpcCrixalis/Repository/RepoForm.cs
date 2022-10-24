using grpcCrixalis.Data;
using Pantheon.Bases.BaseBlazorServer.Repositories.BaseRepoEtm.RepoT0Form;
using Pantheon.Bases.BaseBlazorShared.BaseEtms;

namespace Pantheon.Repository
{
    public class RepoForm : pthRepoT0Form<pthT0Form>, IRepoForm
    {
        public CrixalisDbContext? Context => Db as CrixalisDbContext;
        public RepoForm(CrixalisDbContext db) : base(db)
        {

        }

    }
}
