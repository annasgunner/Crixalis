using Grpc.Net.Client.Web;
using libsCrixalis.Protos;
using Mapster;
using Pantheon.Bases.BaseBlazorShared.BaseEtms;
using Grpc.Net.Client;
using static libsCrixalis.Protos.svpReadJabatan;
using static Pantheon.Utility.modVariabel;

namespace mbaCrixalis._1._Master
{
    public class csvJabatan //Client Service
    {
        private svpReadJabatanClient _readClient { get; set; }
        public csvJabatan()
        {
            var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
            var channel = GrpcChannel.ForAddress(AlamatAPI, new GrpcChannelOptions { HttpClient = httpClient });
            _readClient = new svpReadJabatanClient(channel);
        }

        public IEnumerable<uimT0Jabatan> GetDataJabatan()
        {
            var reply = _readClient.GetAllJabatan(new ReadAllJabatanRequest());
            return reply.ReadJabatanReply.Adapt<IEnumerable<uimT0Jabatan>>();
        }
    }
}
