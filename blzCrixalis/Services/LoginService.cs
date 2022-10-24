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
    public class LoginService : BaseService
    {
        public async Task<LoginTokenReply> Login(string userName, string userPassword)
        {
            async Task<LoginTokenReply> CobaLogin()
            {
                var client = new proLoginToken.proLoginTokenClient(GetGrpcChannel(true));
                LoginTokenRequest loginTokenRequest = new LoginTokenRequest() { UserName = userName, UserPassword = userPassword ?? "" };
                return await client.GetLoginTokenAsync(loginTokenRequest, deadline: DateTime.UtcNow.AddSeconds(30));
            }

            var hasil = await CobaLogin();
            if (hasil is null)
                return new LoginTokenReply();
            return hasil;

        }
    }
}
