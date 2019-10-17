using Diot.Interface;
using Diot.Views.Controls;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XFPlatform = Xamarin.Forms.Platform.iOS.Platform;

[assembly: Xamarin.Forms.Dependency(typeof(Diot.iOS.Implementations.LoadingPageService))]
namespace Diot.iOS.Implementations
{
    /// <summary>
    ///     iOS implementation of the loading page service.
    /// </summary>
    /// <seealso cref="ILoadingPageService" />
    public class LoadingPageService : ILoadingPageService
    {
        #region Fields

        private UIView _nativeView;
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
            if (!_isInitialized && (_currentPage == null || _currentPage.PageText == loadingPageText))
            {
                _currentPage = new LoadingIndicatorPage(loadingPageText);

                initLoadingPage(_currentPage); // set the default page
            }

            // showing the native loading page
            UIApplication.SharedApplication.KeyWindow.AddSubview(_nativeView);

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
            _nativeView.RemoveFromSuperview();

            UpdateText(string.Empty);

            IsShowing = false;
        }

        /// <summary>
        ///     Updates the text on the loading page.
        /// </summary>
        /// <param name="loadingPageText">The loading page text.</param>
        /// <exception cref="System.NotImplementedException"></exception>
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
                renderer = XFPlatform.CreateRenderer(bindable);
                XFPlatform.SetRenderer(bindable, renderer);
            }

            return renderer;
        }

        #endregion
    }
}