using Grpc.Net.Client.Web;
using Pantheon.Protos;
using Mapster;
using static Pantheon.Protos.svpReadForm;
using static Pantheon.Utility.modVariabel;
using Pantheon.Bases.BaseBlazorShared.BaseEtms;
using Grpc.Net.Client;

namespace mbaCrixalis._1._Utilitas
{
    public class csvForm //Client Service
    {
        private svpReadFormClient _readClient { get; set; }
        public csvForm()
        {
            var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
            var channel = GrpcChannel.ForAddress(AlamatAPI, new GrpcChannelOptions { HttpClient = httpClient });
            _readClient = new svpReadFormClient(channel);
        }


        public IEnumerable<pthT0Form> GetDataForm()
        {
            var reply = _readClient.GetForm(new FormRequest());
            return reply.DaftarForm.Adapt<List<pthT0Form>>();
        }
    }
}
