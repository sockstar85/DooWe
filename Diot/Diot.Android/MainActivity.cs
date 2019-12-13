using Android.App;
using Android.Content.PM;
using Android.Graphics;
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

			Xamarin.Essentials.Platform.Init(this, bundle);
            Rg.Plugins.Popup.Popup.Init(this, bundle);
            CrossCurrentActivity.Current.Init(this, bundle);
            Forms.Init(this, bundle);
            AnimationViewRenderer.Init();
            LoadApplication(new App(new AndroidInitializer()));
        }

        /// <summary>
        ///     Called when back pressed.
        /// </summary>
        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                //Do nothing. Popup pages should have a way to close themselves.
            }
            else
            {
                base.OnBackPressed();
            }
        }

		/// <summary>
		///		This allows Xamarin.Essentials to know about permission request results.
		/// </summary>
		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
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