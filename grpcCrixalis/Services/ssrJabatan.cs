using Pantheon.Protos;
using Mapster;
using grpcCrixalis.Data;
using libsCrixalis.Protos;

namespace grpcCrixalis.Services;
public class ssrJabatan : svpReadJabatan.svpReadJabatanBase
{
    private readonly ILogger<ssrJabatan> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public ssrJabatan(ILogger<ssrJabatan> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
#region 'Views'
    public override async Task<ReadAllJabatanReply> GetAllJabatan(ReadAllJabatanRequest request, ServerCallContext context)
    {
        try
        {
            var data = await _unitOfWork.RepoJabatan.GetAll();
            var reply = new ReadAllJabatanReply();
            reply.ReadJabatanReply.AddRange(data.Adapt<IEnumerable<ReadJabatanReply>>());
            return reply;
        }
        catch (Exception e) { throw e; }
    }
    public override async Task<ReadJabatanReply> GetJabatanById(ReadJabatanByIdRequest request, ServerCallContext context)
    {
        try
        {
            var data = await _unitOfWork.RepoJabatan.GetAll();
            var reply = new ReadJabatanReply(data.Adapt<ReadJabatanReply>());
            return reply;
        }
        catch (Exception e) { throw e; }
    }
    #endregion
}
