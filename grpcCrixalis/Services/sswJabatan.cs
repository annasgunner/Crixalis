using Pantheon.Protos;
using Mapster;
using grpcCrixalis.Data;
using libsCrixalis.Protos;
using System.Net;
using libsCrixalis.Master;
using static SQLite.SQLite3;
using System.Runtime.Intrinsics.X86;

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
            var t0Jabatan = request.Adapt<DbT0Jabatan>();
            t0Jabatan.PKLink1 = "";
            t0Jabatan.PKLink2 = "";
            t0Jabatan.IsDefault = false;
            t0Jabatan.IsRVisible = true;
            t0Jabatan.IsDVisible = true;
            t0Jabatan.Tag = "";
            t0Jabatan.Status = true;
            t0Jabatan.WaktuInsert = DateTimeOffset.Now;
            t0Jabatan.IdCreator = Guid.Parse(request.IdCreator);
            t0Jabatan.State = "";
            t0Jabatan.Synchronise = "inserted";

            _unitOfWork.RepoJabatan.Insert(t0Jabatan);
            await _unitOfWork.Selesai();
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
            //DbT0Jabatan t0Jabatan;
            var DbT0Jabatan = _unitOfWork.RepoJabatan.GetById(request.IdJabatan);
            if (DbT0Jabatan is not null)
            {
                DbT0Jabatan.Result.Jabatan = request.Jabatan;
                DbT0Jabatan.Result.Kode = request.Kode;
                DbT0Jabatan.Result.Grade = request.Grade;
                DbT0Jabatan.Result.Keterangan = request.Keterangan;
                DbT0Jabatan.Result.WaktuUpdate = DateTimeOffset.Now;
                //Guid.TryParse(request.IdOperator, out var idOperator);
                //t0Jabatan.IdOperator = idOperator;
                //Guid.TryParse(request.IdOperator, out var idValidator);
                //t0Jabatan.IdValidator = idValidator;
                DbT0Jabatan.Result.Synchronise = "updated";

                //_unitOfWork.RepoJabatan.Update(DbT0Jabatan);
                await _unitOfWork.Selesai();
            }
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
            await _unitOfWork.Selesai();
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
