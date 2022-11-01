using Pantheon.Protos;
using Mapster;
using grpcCrixalis.Data;

namespace grpcCrixalis.Services
{
    public class ssvForm : svpReadForm.svpReadFormBase
    {
        private readonly ILogger<ssvForm> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public ssvForm(ILogger<ssvForm> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public override async Task<rpfForm> GetForm(FormRequest request, ServerCallContext context)
        {
            try
            {
                var data = await _unitOfWork.RepoForm.CariDenganPredicate(f => (f.Form != null || f.IdParent == 0) && f.Status == true);
                var reply = new rpfForm();
                reply.DaftarForm.AddRange(data.Adapt<IEnumerable<proForm>>());
                return reply;
            }
            catch (Exception e) { throw e; }
        }
    }
}
