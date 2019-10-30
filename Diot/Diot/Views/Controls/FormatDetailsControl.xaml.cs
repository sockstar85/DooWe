using Diot.Interface;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ResourceManager = Diot.Services.ResourceManager;

namespace Diot.Views.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FormatDetailsControl
	{
        #region Fields

	    private IResourceManager _resourceManager => ResourceManager.Instance;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is on Bluray.
        /// </summary>
        public bool IsOnBluray
	    {
	        get => (bool) GetValue(IsOnBlurayProperty);
	        set => SetValue(IsOnBlurayProperty, value);
	    }

	    /// <summary>
	    ///     Gets or sets a value indicating whether this instance is on Dvd.
	    /// </summary>
	    public bool IsOnDvd
	    {
	        get => (bool)GetValue(IsOnDvdProperty);
	        set => SetValue(IsOnDvdProperty, value);
	    }

	    /// <summary>
	    ///     Gets or sets a value indicating whether this instance is on Vudu.
	    /// </summary>
	    public bool IsOnVudu
	    {
	        get => (bool)GetValue(IsOnVuduProperty);
	        set => SetValue(IsOnVuduProperty, value);
	    }

	    /// <summary>
	    ///     Gets or sets a value indicating whether this instance is on Movies Anywhere.
	    /// </summary>
	    public bool IsOnMoviesAnywhere
	    {
	        get => (bool)GetValue(IsOnMoviesAnywhereProperty);
	        set => SetValue(IsOnMoviesAnywhereProperty, value);
	    }

	    /// <summary>
	    ///     Gets or sets a value indicating whether this instance is on Amazon.
	    /// </summary>
	    public bool IsOnAmazon
	    {
	        get => (bool)GetValue(IsOnAmazonProperty);
	        set => SetValue(IsOnAmazonProperty, value);
	    }
	    /// <summary>
	    ///     Gets or sets a value indicating whether this instance is on Plex.
	    /// </summary>
	    public bool IsOnPlex
	    {
	        get => (bool)GetValue(IsOnPlexProperty);
	        set => SetValue(IsOnPlexProperty, value);
	    }

	    /// <summary>
	    ///     Gets or sets a value indicating whether this instance is on another format.
	    /// </summary>
	    public bool IsOnOther
	    {
	        get => (bool)GetValue(IsOnOtherProperty);
	        set => SetValue(IsOnOtherProperty, value);
	    }

        /// <summary>
        ///     Gets or sets the other format.
        /// </summary>
        public string OtherFormat
	    {
	        get => (string) GetValue(OtherFormatProperty);
	        set => SetValue(OtherFormatProperty, value);
	    }

        #endregion

        #region BindableProperties

        /// <summary>
        ///     The bindable property for <see cref="IsOnBluray"/>.
        /// </summary>
        public static readonly BindableProperty IsOnBlurayProperty =
	        BindableProperty.Create(
	            nameof(IsOnBluray),
	            typeof(bool),
	            typeof(FormatDetailsControl),
	            default(bool),
	            propertyChanged:
	            (bindable, oldValue, newValue) =>
	                ((FormatDetailsControl) bindable).updateIsOnBluray());

	    /// <summary>
	    ///     The bindable property for <see cref="IsOnDvd"/>.
	    /// </summary>
	    public static readonly BindableProperty IsOnDvdProperty =
	        BindableProperty.Create(
	            nameof(IsOnDvd),
	            typeof(bool),
	            typeof(FormatDetailsControl),
	            default(bool),
	            propertyChanged:
	            (bindable, oldValue, newValue) =>
	                ((FormatDetailsControl)bindable).updateIsOnDvd());

        /// <summary>
        ///     The bindable property for <see cref="IsOnVudu"/>.
        /// </summary>
        public static readonly BindableProperty IsOnVuduProperty =
	        BindableProperty.Create(
	            nameof(IsOnVudu),
	            typeof(bool),
	            typeof(FormatDetailsControl),
	            default(bool),
	            propertyChanged:
	            (bindable, oldValue, newValue) =>
	                ((FormatDetailsControl)bindable).updateIsOnVudu());

	    /// <summary>
	    ///     The bindable property for <see cref="IsOnMoviesAnywhere"/>.
	    /// </summary>
	    public static readonly BindableProperty IsOnMoviesAnywhereProperty =
	        BindableProperty.Create(
	            nameof(IsOnMoviesAnywhere),
	            typeof(bool),
	            typeof(FormatDetailsControl),
	            default(bool),
	            propertyChanged:
	            (bindable, oldValue, newValue) =>
	                ((FormatDetailsControl)bindable).updateIsOnMoviesAnywhere());

	    /// <summary>
	    ///     The bindable property for <see cref="IsOnAmazon"/>.
	    /// </summary>
	    public static readonly BindableProperty IsOnAmazonProperty =
	        BindableProperty.Create(
	            nameof(IsOnAmazon),
	            typeof(bool),
	            typeof(FormatDetailsControl),
	            default(bool),
	            propertyChanged:
	            (bindable, oldValue, newValue) =>
	                ((FormatDetailsControl)bindable).updateIsOnAmazon());

	    /// <summary>
	    ///     The bindable property for <see cref="IsOnPlex"/>.
	    /// </summary>
	    public static readonly BindableProperty IsOnPlexProperty =
	        BindableProperty.Create(
	            nameof(IsOnPlex),
	            typeof(bool),
	            typeof(FormatDetailsControl),
	            default(bool),
	            propertyChanged:
	            (bindable, oldValue, newValue) =>
	                ((FormatDetailsControl)bindable).updateIsOnPlex());

	    /// <summary>
	    ///     The bindable property for <see cref="IsOnOther"/>.
	    /// </summary>
	    public static readonly BindableProperty IsOnOtherProperty =
	        BindableProperty.Create(
	            nameof(IsOnOther),
	            typeof(bool),
	            typeof(FormatDetailsControl),
	            default(bool),
	            propertyChanged:
	            (bindable, oldValue, newValue) =>
	                ((FormatDetailsControl)bindable).updateIsOnOther());

	    /// <summary>
	    ///     The bindable property for <see cref="OtherFormat"/>.
	    /// </summary>
	    public static readonly BindableProperty OtherFormatProperty =
	        BindableProperty.Create(
	            nameof(OtherFormat),
	            typeof(string),
	            typeof(FormatDetailsControl),
	            default(string),
	            propertyChanged:
	            (bindable, oldValue, newValue) =>
	                ((FormatDetailsControl)bindable).updateOtherFormat());
        
        #endregion

        #region Methods

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="FormatDetailsControl"/> class.
        /// </summary>
        public FormatDetailsControl()
	    {
	        InitializeComponent();
	    }

        #endregion

        /// <summary>
        ///     Updates the bluray image visibility.
        /// </summary>
        private void updateIsOnBluray()
	    {
			BlurayImg.IsVisible = IsOnBluray;
        }

        /// <summary>
        ///     Updates the dvd image visibility.
        /// </summary>
        private void updateIsOnDvd()
	    {
			DvdImg.IsVisible = IsOnDvd;
	    }

	    /// <summary>
	    ///     Updates the Vudu image visibility.
	    /// </summary>
	    private void updateIsOnVudu()
	    {
			VuduImg.IsVisible = IsOnVudu;
	    }

	    /// <summary>
	    ///     Updates the Movies Anywhere image visibility.
	    /// </summary>
	    private void updateIsOnMoviesAnywhere()
	    {
			MoviesAnywhereImg.IsVisible = IsOnMoviesAnywhere;
	    }

	    /// <summary>
	    ///     Updates the Amazon image visibility.
	    /// </summary>
	    private void updateIsOnAmazon()
	    {
			AmazonImg.IsVisible = IsOnAmazon;
	    }

	    /// <summary>
	    ///     Updates the Plex image visibility.
	    /// </summary>
	    private void updateIsOnPlex()
	    {
			PlexImg.IsVisible = IsOnPlex;
	    }

	    /// <summary>
	    ///     Updates the other label visibility.
	    /// </summary>
	    private void updateIsOnOther()
	    {
			OtherLabel.IsVisible = IsOnOther;
	    }

        /// <summary>
        ///     Updates the other format label text.
        /// </summary>
        private void updateOtherFormat()
        {
            OtherFormatSpan.Text = $"\n{OtherFormat}";
        }

        #endregion

    }
}