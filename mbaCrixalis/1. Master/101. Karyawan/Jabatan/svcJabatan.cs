using Grpc.Net.Client.Web;
using libsCrixalis.Protos;
using Mapster;
using Pantheon.Bases.BaseBlazorShared.BaseEtms;
using Grpc.Net.Client;
using static libsCrixalis.Protos.svpReadJabatan;
using static libsCrixalis.Protos.svpWriteJabatan;
using static Pantheon.Utility.modVariabel;

namespace mbaCrixalis._1._Master
{
    public class svcJabatan
    {
        private svpReadJabatanClient _readClient { get; set; }
        private svpWriteJabatanClient _writeClient { get; set; }
        public svcJabatan()
        {
            _readClient = new svpReadJabatanClient(ClientChannel);
            _writeClient = new svpWriteJabatanClient(ClientChannel);
        }

        public IEnumerable<uimT0Jabatan> GetDataJabatan()
        {
            var reply = _readClient.GetAllJabatan(new ReadAllJabatanRequest());
            return reply.ReadJabatanReply.Adapt<IEnumerable<uimT0Jabatan>>();
        }
        public async Task<bool> InsertJabatan(uimT0Jabatan Jabatan)
        {
            var request = Jabatan.Adapt<WriteJabatanRequest>();
            request.IdKategoriJadwalKerjaKaryawan = Guid.Empty.ToString();
            request.IdCreator = Guid.Empty.ToString();
            request.IdOperator = Guid.Empty.ToString();
            request.IdValidator = Guid.Empty.ToString();
            var reply = await _writeClient.InsertJabatanAsync(request);
            return reply.IsOK;
        }

        public async Task<bool> UpdateJabatan(uimT0Jabatan Jabatan)
        {
            var request = Jabatan.Adapt<WriteJabatanRequest>();
            var reply = await _writeClient.UpdateJabatanAsync(request);
            return reply.IsOK;
        }

        public async Task<bool> DeleteJabatan(Guid idJabatan)
        {
            var request = new WriteJabatanByIdRequest { IdJabatan = idJabatan.ToString() };
            var reply = await _writeClient.DeleteJabatanAsync(request);
            return reply.IsOK;
        }
    }
}
