using System;
using Diot.Interface;
using Diot.Services;
using Xamarin.Forms;
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
        #region Fields

        private static IResourceManager _resourceManager => ResourceManager.Instance;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the page text.
        /// </summary>
        public string PageText
	    {
	        get => (string) GetValue(PageTextProperty);
	        set => SetValue(PageTextProperty, value);
	    }

        #endregion

        #region Bindable Properties

        /// <summary>
        ///     Bindable property for <see cref="PageTextProperty"/>.
        /// </summary>
        public static readonly BindableProperty PageTextProperty =
            BindableProperty.Create(
                nameof(PageText),
                typeof(string),
                typeof(LoadingIndicatorPage),
                _resourceManager.GetString("Loading"),
                propertyChanged: (bindable, oldValue, newValue) =>
                    ((LoadingIndicatorPage)bindable).updatePageText());
        
        #endregion

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

        /// <summary>
        ///     Updates the page text.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void updatePageText()
        {
            LoadingPageText.Text = PageText;
        }

        #endregion
    }
}