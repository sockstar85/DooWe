using Xamarin.Forms.Xaml;

namespace Diot.Views.Controls
{
    /// <summary>
    ///     Loading indicator page.
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoadingIndicatorPage
	{
        #region Methods

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="LoadingIndicatorPage"/> class.
        /// </summary>
        public LoadingIndicatorPage (string loadingPageText)
		{
			InitializeComponent ();
		    LoadingPageText.Text = loadingPageText;
		}

        #endregion

        /// <summary>
        ///     Called when the loading page is appearing. Starts the animation.
        /// </summary>
        protected override void OnAppearing()
	    {
            base.OnAppearing();

	        if (LottieAnimation != null)
	        {
	            LottieAnimation.IsPlaying = true;
	        }
	    }

        /// <summary>
        ///     Called when the loading page is disappearing. Stops the animation.
        /// </summary>
        protected override void OnDisappearing()
	    {
	        if (LottieAnimation != null)
	        {
	            LottieAnimation.IsPlaying = false;
	        }

            base.OnDisappearing();
	    }

        #endregion
    }
}