using Android.App;
using Android.Graphics.Drawables;
using Android.Views;
using Diot.Interface;
using Diot.Views.Controls;
using Xamarin.Forms;
using Plugin.CurrentActivity;
using Xamarin.Forms.Platform.Android;
using XFPlatform = Xamarin.Forms.Platform.Android.Platform;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(Diot.Droid.Implementations.LoadingPageService))]
namespace Diot.Droid.Implementations
{
    /// <summary>
    ///     Android's implementation of the loading page service.
    /// </summary>
    /// <seealso cref="ILoadingPageService" />
    public class LoadingPageService : ILoadingPageService
    {
        #region Fields

        private Android.Views.View _nativeView;
        private Dialog _dialog;
        private bool _isInitialized;
        private LoadingIndicatorPage _currentPage;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets a value indicating whether this instance is showing.
        /// </summary>
        public bool IsShowing { get; private set; }

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

                _nativeView = renderer.View;

                _dialog = new Dialog(CrossCurrentActivity.Current.Activity);
                _dialog.RequestWindowFeature((int)WindowFeatures.NoTitle);
                _dialog.SetCancelable(false);
                _dialog.SetContentView(_nativeView);
                Window window = _dialog.Window;
                window.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                window.ClearFlags(WindowManagerFlags.DimBehind);
                window.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.Transparent));

                _isInitialized = true;
            }
        }

        /// <summary>
        ///     Shows the loading page.
        /// </summary>
        /// <param name="loadingPageText">The loading page text.</param>
        /// <param name="delay">The delay.</param>
        /// <param name="callerName">Name of the caller.</param>
        public void ShowLoadingPage(string loadingPageText, int delay = 0, [CallerMemberName] string callerName = null)
        {
            // check if the user has set the page or not
            if (!_isInitialized && (_currentPage == null || _currentPage.PageText == loadingPageText))
            {
                _currentPage = new LoadingIndicatorPage(loadingPageText);
                initLoadingPage(_currentPage); // set the default page
            }

            // showing the native loading page
            _dialog.Show();

            IsShowing = true;
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
            _dialog.Hide();

            UpdateText(string.Empty);

            IsShowing = false;
        }

        /// <summary>
        ///     Updates the text on the loading page.
        /// </summary>
        /// <param name="loadingPageText">The loading page text.</param>
        public void UpdateText(string loadingPageText)
        {
            if (_currentPage == null)
            {
                return;
            }
            _currentPage.PageText = loadingPageText;
        }

        #endregion
    }

    #region Helper class

    /// <summary>
    ///     Internal loading page service helper class.
    /// </summary>
    internal static class LoadingPageServiceHelper
    {
        /// <summary>
        ///     Extension method that gets the or create renderer.
        /// </summary>
        /// <param name="bindable">The bindable.</param>
        /// <returns></returns>
        public static IVisualElementRenderer GetOrCreateRenderer(this VisualElement bindable)
        {
            var renderer = XFPlatform.GetRenderer(bindable);
            if (renderer == null)
            {
                var currentActivity = CrossCurrentActivity.Current.Activity;

                renderer = XFPlatform.CreateRendererWithContext(bindable, currentActivity);
                XFPlatform.SetRenderer(bindable, renderer);
            }

            return renderer;
        }
    }

    #endregion
}