using Pantheon.Protos;
using Mapster;
using grpcCrixalis.Data;

namespace grpcCrixalis.Services
{
    public class svcForm : svpReadForm.svpReadFormBase
    {
        private readonly ILogger<svcForm> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public svcForm(ILogger<svcForm> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public override async Task<rpfForm> GetForm(FormRequest request, ServerCallContext context)
        {
            try
            {
                var data = await _unitOfWork.RepoForm.CariDenganPredicate(f => f.Form != null || f.IdParent == 0);
                var reply = new rpfForm();
                reply.DaftarForm.AddRange(data.Adapt<IEnumerable<proForm>>());
                return reply;
            }
            catch (Exception e) { throw e; }
        }
    }
}
