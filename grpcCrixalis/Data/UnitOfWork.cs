using Pantheon.Bases.BaseBlazorServer.DataAccess;

namespace grpcCrixalis.Data
{
    public class UnitOfWork : pthUnitOfWork, IUnitOfWork
    {
        private readonly CrixalisDbContext _context;
        public IRepoForm RepoForm { get; private set; }
        public IRepoJabatan RepoJabatan { get; private set; }
        public UnitOfWork(CrixalisDbContext context) : base(context)
        {
            _context = context;
            RepoForm = new RepoForm(_context);
            RepoJabatan = new RepoJabatan(_context);
        }
    }
}
