global using Mapster;
global using Grpc.Core;
global using Microsoft.EntityFrameworkCore;
using grpcCrixalis;
using grpcCrixalis.Data;
using grpcCrixalis.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pantheon.Services.Server;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Pantheon.Bases.BaseBlazorServer.DataAccess;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
services.AddGrpc();
services.AddCors(o => o.AddPolicy("AllowAll", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()
           .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
}));
var config = TypeAdapterConfig.GlobalSettings;
config.Default.NameMatchingStrategy(NameMatchingStrategy.Flexible);
config.Default.IgnoreNullValues(true);
builder.Services.AddSingleton(config);

services.AddControllersWithViews();
services.AddRazorPages();

//Config Database
services.AddDbContext<CrixalisDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

ConfigurationManager configuration = builder.Configuration;
var appSettingSection = configuration.GetSection("AppSettings");
services.Configure<AppSettings>(appSettingSection);

var appSettings = appSettingSection.Get<AppSettings>();
var key = Encoding.UTF8.GetBytes(appSettings.Secret);

//Add JWT Bearer
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddAuthorization();

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
if (!(!string.IsNullOrWhiteSpace(environment) && environment.Contains("Development")))
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        // Setup a HTTP/2 endpoint without TLS.
        options.ListenAnyIP(5050, o =>
        {
            o.Protocols = HttpProtocols.Http1AndHttp2;
        });
        options.ListenAnyIP(5021, o =>
        {
            o.Protocols = HttpProtocols.Http1AndHttp2;
            o.UseHttps();
        });
    });
}

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
var app = builder.Build();

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseGrpcWeb();
app.UseCors();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<LoginTokenService>().EnableGrpcWeb();
    endpoints.MapGrpcService<KaryawanService>().EnableGrpcWeb();
    endpoints.MapGrpcService<ssvForm>().EnableGrpcWeb();
    endpoints.MapGrpcService<ssrJabatan>().EnableGrpcWeb();
    endpoints.MapGrpcService<sswJabatan>().EnableGrpcWeb();
    // Configure the HTTP request pipeline.
    //endpoints.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
});

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
