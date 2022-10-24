using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using System.Threading.Channels;
using Grpc.Net.Client.Web;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using grpcCrixalis.Protos;

namespace blzCrixalis.Services
{
    public abstract class BaseService : IBaseService
    {
        public string Token { get; set; }
        public string BaseApiAddress { get; set; }
        private GrpcChannel grpcChannel;

        private static bool _online = false;
        public bool Online { get => _online; }

        private static Metadata _headers;
        public Metadata Headers { get => _headers; }

        public BaseService()
        {
            BaseApiAddress = "https://localhost:5001";
        }

        public virtual async Task<bool> KoneksiReady(double timeOut = 3)
        {
            var channel = GetGrpcChannel();
            try
            {
                _online = false;
                await channel.ConnectAsync();
                _online = channel.State == ConnectivityState.Ready;

                return _online;
            }
            catch (TaskCanceledException tce)
            {
                _online = false;
                return false;
            }
            finally
            {
                await channel.ShutdownAsync();
            }
        }

        public GrpcChannel GetGrpcChannel(bool useAuthorization = false)
        {
            _headers = new Metadata
            {
                { "Authorization", $"Bearer {Token}" }
            };

            //grpcChannel = GrpcChannel.ForAddress(new Uri(BaseApiAddress));

            var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
            grpcChannel = GrpcChannel.ForAddress(new Uri(BaseApiAddress), new GrpcChannelOptions { HttpClient = httpClient });

            return grpcChannel;
        }

    }
}
