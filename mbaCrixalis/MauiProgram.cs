using Radzen;
using mbaCrixalis._1__Utilitas;
using mbaCrixalis._1._Utilitas;
using Pantheon;
using static Pantheon.Utility.modVariabel;
using mbaCrixalis._1._Master;

namespace mbaCrixalis;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        AlamatAPI = "https://localhost:5021";

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiPantheon()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif

        builder.Services.AddSingleton<csvForm>();
        builder.Services.AddSingleton<svcJabatan>();
        //builder.Services.AddSingleton<pthSignalRService>();
        builder.Services.AddScoped<DialogService>();
        builder.Services.AddScoped<NotificationService>();
        builder.Services.AddScoped<TooltipService>();
        builder.Services.AddScoped<ContextMenuService>();
        builder.Services.AddDevExpressBlazor(configure => configure.BootstrapVersion = DevExpress.Blazor.BootstrapVersion.v5);

        return builder.Build();
    }
}
