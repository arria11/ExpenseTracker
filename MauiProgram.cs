using ExpenseTracker.Services;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddTransient<ITransactionService, TransactionService>();
            builder.Services.AddSingleton<AuthenticationStateService>();
            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton<TagService>();
            builder.Services.AddTransient<CalculateTransactionService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
