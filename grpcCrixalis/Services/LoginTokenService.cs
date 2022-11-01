using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using grpcCrixalis.Data;
using libsCrixalis.Protos;
using libsCrixalis.Master;
using grpcCrixalis.Utility;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace grpcCrixalis.Services
{
    public class LoginTokenService : proLoginToken.proLoginTokenBase
    {
        private readonly ILogger<LoginTokenService> _logger;
        private readonly CrixalisDbContext _db;
        private readonly ClsRijndael _clsRijndael;
        private readonly AppSettings _appSettings;
        private readonly ClsErrorHandling _clsErrorHandling;

        public LoginTokenService(ILogger<LoginTokenService> logger, CrixalisDbContext db, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _db = db;
            _clsRijndael = new ClsRijndael();
            _appSettings = appSettings.Value;
            _clsErrorHandling = new ClsErrorHandling();
        }

        public override async Task<LoginTokenReply> GetLoginToken(LoginTokenRequest request, ServerCallContext context)
        {
            var userName = request.UserName;
            var userPassword = Encoding.UTF8.GetBytes(_clsRijndael.Encrypt(_clsRijndael.Encrypt(request.UserPassword)));
            LoginTokenReply hasil = new LoginTokenReply();
            _logger.LogInformation($"User {userName} is loggin in");

            try
            {
                var user = _db.T1Karyawan
                              .Where(w => w.UserName == userName)
                              .Select(k => new
                              {
                                  k.IdKaryawan,
                                  k.NamaLengkap,
                                  k.NamaPanggilan
                              })
                              .FirstOrDefault();

                // Jika Username tidak ditemukan
                if (user is null)
                {
                    //var metadata = new Metadata() { { "Error", "Username yg anda masukkan salah !" } };
                    //throw new RpcException(new Status(StatusCode.NotFound, "Not Found"), metadata);

                    hasil.Message = $"Username yg anda masukkan salah !";
                    _logger.LogInformation(hasil.Message);

                    return hasil;
                }
                _clsErrorHandling.CheckCancelRequest(context, _db);

                var pass = _db.T1Karyawan
                              .Where(w => w.UserName == userName &&
                                          w.UserPassword == userPassword)
                              .Select(k => new
                              {
                                  k.IdKaryawan,
                                  k.NamaLengkap,
                                  k.NamaPanggilan
                              })
                              .FirstOrDefault();

                // Jika Password salah
                if (pass is null)
                {
                    //var metadata = new Metadata() { { "Error", "Username yang anda masukkan salah !" } };
                    //throw new RpcException(new Status(StatusCode.NotFound, "Not Found"), metadata);

                    hasil.Message = $"Password yang anda masukkan salah !";
                    _logger.LogInformation(hasil.Message);

                    return hasil;
                }
                _clsErrorHandling.CheckCancelRequest(context, _db);

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.IdKaryawan.ToString()),
                        new Claim(ClaimTypes.Name, user.NamaLengkap ?? "")
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                _clsErrorHandling.CheckCancelRequest(context, _db);

                if (user != null)
                {
                    hasil.Message = $"Login berhasil !";
                    hasil.Token = tokenHandler.WriteToken(token);
                    hasil.IdKaryawan = user.IdKaryawan.ToString();
                    hasil.NamaLengkap = user.NamaLengkap;
                    hasil.NamaPanggilan = user.NamaPanggilan;

                    _logger.LogInformation(hasil.Message);
                }
            }
            catch (Exception ex)
            {
                Metadata metadata = new() { { "error", "Error : " + ex.Message } };
                throw new RpcException(new Status(StatusCode.Unauthenticated, "Unauthenticated"), metadata);
            }

            return hasil;
        }
    }
}
