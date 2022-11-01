using Pantheon.Protos;
using Mapster;
using grpcCrixalis.Data;
using libsCrixalis.Protos;
using System.Net;
using libsCrixalis.Master;
using static SQLite.SQLite3;

namespace grpcCrixalis.Services;
public class sswJabatan : svpWriteJabatan.svpWriteJabatanBase
{
    private readonly ILogger<sswJabatan> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public sswJabatan(ILogger<sswJabatan> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
#region 'Procedure'
    public override async Task<WriteJabatanReply> InsertJabatan(WriteJabatanRequest request, ServerCallContext context)
    {
        try
        {
            _unitOfWork.RepoJabatan.Insert(request.Adapt<DbT0Jabatan>());
            return new WriteJabatanReply() { IsOK = true, Result = "Berhasil Simpan" };
        }
        catch (RpcException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            Metadata metadata = new() { { "error", "Error : " + ex.Message } };
            throw new RpcException(new Status(StatusCode.Unknown, "Unknown"), metadata);
        }
    }
    public override async Task<WriteJabatanReply> UpdateJabatan(WriteJabatanRequest request, ServerCallContext context)
    {
        try
        {
            _unitOfWork.RepoJabatan.Update(request.Adapt<DbT0Jabatan>());
            return new WriteJabatanReply() { IsOK = true, Result = "Berhasil Simpan" };
        }
        catch (RpcException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            Metadata metadata = new() { { "error", "Error : " + ex.Message } };
            throw new RpcException(new Status(StatusCode.Unknown, "Unknown"), metadata);
        }
    }
    public override async Task<WriteJabatanReply> DeleteJabatan(WriteJabatanByIdRequest request, ServerCallContext context)
    {
        try
        {
            _unitOfWork.RepoJabatan.Delete(request.Adapt<DbT0Jabatan>());
            return new WriteJabatanReply() { IsOK = true, Result = "Berhasil Hapus" };
        }
        catch (RpcException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            Metadata metadata = new() { { "error", "Error : " + ex.Message } };
            throw new RpcException(new Status(StatusCode.Unknown, "Unknown"), metadata);
        }
    }
    #endregion
}
