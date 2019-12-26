using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Diot.Views.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FormatSelectorControl : ContentView
	{
        #region Properties

	    /// <summary>
	    ///     Gets or sets the image.
	    /// </summary>
	    public ImageSource Image
	    {
	        get => (ImageSource)GetValue(ImageProperty);
	        set => SetValue(ImageProperty, value);
	    }

	    /// <summary>
	    ///     Gets or sets a value indicating whether this instance is selected.
	    /// </summary>
	    public bool IsSelected
	    {
	        get => (bool)GetValue(IsSelectedProperty);
	        set => SetValue(IsSelectedProperty, value);
	    }

		/// <summary>
		///		Gets or sets the color of the tint.
		/// </summary>
		public Color TintColor
		{
			get => (Color)GetValue(TintColorProperty);
			set => SetValue(TintColorProperty, value);
		}

		#endregion

		#region Bindable Properties

		/// <summary>
		///		The bindable property for <see cref="TintColor"/>.
		/// </summary>
		public static readonly BindableProperty TintColorProperty =
			BindableProperty.Create(
				nameof(TintColor),
				typeof(Color),
				typeof(FormatSelectorControl),
				Color.Transparent,
				propertyChanged:
				(bindable, oldValue, newValue) =>
					((FormatSelectorControl)bindable).updateTintColor());

	    /// <summary>
	    ///     The bindable property for <see cref="Image"/>.
	    /// </summary>
	    public static readonly BindableProperty ImageProperty =
	        BindableProperty.Create(
	            nameof(Image),
	            typeof(ImageSource),
	            typeof(FormatSelectorControl),
	            default(ImageSource),
	            propertyChanged: 
	            (bindable, oldValue, newValue) =>
	                ((FormatSelectorControl)bindable).updateImage());

        /// <summary>
        ///     The bindable property for <see cref="IsSelected"/>.
        /// </summary>
        public static readonly BindableProperty IsSelectedProperty =
	        BindableProperty.Create(
	            nameof(IsSelected),
	            typeof(bool),
	            typeof(FormatSelectorControl),
	            default(bool),
	            propertyChanged:
	            (bindable, oldValue, newValue) =>
	                ((FormatSelectorControl)bindable).updateIsSelected());

        #endregion

        #region Methods

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="FormatSelectorControl"/> class.
        /// </summary>
        public FormatSelectorControl ()
		{
			InitializeComponent ();
		}

		#endregion

		/// <summary>
		///		Updates the color of the tint.
		/// </summary>
		private void updateTintColor()
		{
			FormatImage.TintColor = TintColor;
		}

	    /// <summary>
	    ///     Updates the control opacity indicating it's selected.
	    /// </summary>
	    private void updateIsSelected()
	    {
            Control.FadeTo(IsSelected ? 1 : 0.3, easing: Easing.SinInOut);
	    }

	    /// <summary>
	    ///     Updates the image source.
	    /// </summary>
	    private void updateImage()
	    {
	        FormatImage.Source = Image;
	    }

        #endregion

        #region Events

        /// <summary>
        ///     Called when tapped.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnTapped(object sender, EventArgs e)
	    {
	        IsSelected = !IsSelected;
	    }

        #endregion
    }
}