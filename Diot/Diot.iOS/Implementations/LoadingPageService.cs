using Diot.Interface;
using Diot.iOS.Implementations;
using Diot.Views.Controls;
using System.Runtime.CompilerServices;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XFPlatform = Xamarin.Forms.Platform.iOS.Platform;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(Diot.iOS.Implementations.LoadingPageService))]
namespace Diot.iOS.Implementations
{
    /// <summary>
    ///     iOS implementation of the loading page service. //TODO: Figure out why the dependency service isn't working on iOS.
    /// </summary>
    /// <seealso cref="ILoadingPageService" />
    public class LoadingPageService : ILoadingPageService
    {
        #region Fields

        private UIView _nativeView;
        private bool _isInitialized;

        #endregion

        #region Methods

        /// <summary>
        ///     Initializes the loading page.
        /// </summary>
        /// <param name="loadingIndicatorPage">The loading indicator page.</param>
        private void initLoadingPage(ContentPage loadingIndicatorPage)
        {
            // check if the page parameter is available
            if (loadingIndicatorPage != null)
            {
                // build the loading page with native base
                loadingIndicatorPage.Parent = Xamarin.Forms.Application.Current.MainPage;

                loadingIndicatorPage.Layout(new Rectangle(0, 0,
                    Xamarin.Forms.Application.Current.MainPage.Width,
                    Xamarin.Forms.Application.Current.MainPage.Height));

                var renderer = loadingIndicatorPage.GetOrCreateRenderer();

                _nativeView = renderer.NativeView;

                _isInitialized = true;
            }
        }

        /// <summary>
        ///     Shows the loading page.
        /// </summary>
        public void ShowLoadingPage(string loadingPageText, int delay = 0, [CallerMemberName] string callerName = null)
        {
            // check if the user has set the page or not
            if (!_isInitialized)
            {
                initLoadingPage(new LoadingIndicatorPage(loadingPageText)); // set the default page
            }

            // showing the native loading page
            UIApplication.SharedApplication.KeyWindow.AddSubview(_nativeView);
        }

        /// <summary>
        ///     Hides the loading page.
        /// </summary>
        /// <param name="delay">The delay.</param>
        /// <param name="callername">The callername.</param>
        public async Task HideLoadingPageAsync(int delay = 0, [CallerMemberName] string callername = null)
        {
            if (delay > 0)
            {
                await Task.Delay(delay);
            }

            // Hide the page
            _nativeView.RemoveFromSuperview();
        }

        #endregion
    }

    /// <summary>
    ///     Internal loading page service helper.
    /// </summary>
    internal static class LoadingPageServiceHelper
    {
        #region Methods

        /// <summary>
        ///     Extension method that gets or creates the  renderer.
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        public static IVisualElementRenderer GetOrCreateRenderer(this VisualElement bindable)
        {
            var renderer = XFPlatform.GetRenderer(bindable);
            if (renderer == null)
            {
                renderer = XFPlatform.GetRenderer(bindable);
                XFPlatform.SetRenderer(bindable, renderer);
            }

            return renderer;
        }

        #endregion
    }
}