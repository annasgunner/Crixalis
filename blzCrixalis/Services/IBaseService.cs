using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace blzCrixalis.Services
{
    public interface IBaseService
    {
        bool Online { get; }
        Task<bool> KoneksiReady(double timeOut = 3);
        GrpcChannel GetGrpcChannel(bool login = false);
    }
}
