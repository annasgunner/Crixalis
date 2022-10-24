using Grpc.Net.Client;
using grpcCrixalis.Protos;
using libsCrixalis.Protos;
using grpcCrixalis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blzCrixalis.Services
{
    public class KaryawanService : BaseService
    {
        public async Task<ReadAllKaryawanReply> GetAllKaryawan()
        {
            var client = new ReadKaryawan.ReadKaryawanClient(GetGrpcChannel(true));
            ReadAllKaryawanRequest loginTokenRequest = new ReadAllKaryawanRequest() { };

            var hasil = await client.GetAllKaryawanAsync(loginTokenRequest, deadline: DateTime.UtcNow.AddSeconds(30));
            if (hasil is null)
                return new ReadAllKaryawanReply();
            return hasil;
        }
        public async Task<ReadKaryawanReply> GetKaryawanById(Guid idKaryawan)
        {
            var client = new ReadKaryawan.ReadKaryawanClient(GetGrpcChannel(true));
            ReadKaryawanByIdRequest loginTokenRequest = new ReadKaryawanByIdRequest() { IdKaryawan = idKaryawan.ToString() };

            var hasil = await client.GetKaryawanByIdAsync(loginTokenRequest, deadline: DateTime.UtcNow.AddSeconds(30));
            if (hasil is null)
                return new ReadKaryawanReply();
            return hasil;
        }
    }
}
