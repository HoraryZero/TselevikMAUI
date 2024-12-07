using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using Plugin.Fingerprint;
using TselevikMAUI;

namespace TselevikMAUI {
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            ConfigurePlatformSpecifics(builder);

            return builder.Build();
        }

        private static void ConfigurePlatformSpecifics(MauiAppBuilder builder)
        {
            builder.ConfigureLifecycleEvents(lifecycle =>
            {
#if ANDROID
                lifecycle.AddAndroid(android =>
                {
                    android.OnCreate((activity, bundle) =>
                    {
                        CrossFingerprint.SetCurrentActivityResolver(() => Platform.CurrentActivity);
                    });
                });
#endif
            });
        }
    }
}
