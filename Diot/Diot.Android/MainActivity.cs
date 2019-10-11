using Android.App;
using Android.Content.PM;
using Android.OS;
using Diot.Droid.Implementations;
using Diot.Interface;
using Lottie.Forms.Droid;
using Plugin.CurrentActivity;
using Prism;
using Prism.Ioc;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Diot.Droid
{
    [Activity(Label = "DoWee", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme.Base", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        #region Methods

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            CrossCurrentActivity.Current.Init(this, bundle);
            Forms.Init(this, bundle);
            AnimationViewRenderer.Init();
            LoadApplication(new App(new AndroidInitializer()));
        }

        #endregion
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        #region Methods

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
            containerRegistry.RegisterSingleton<ILoadingPageService, LoadingPageService>();
        }

        #endregion
    }
}