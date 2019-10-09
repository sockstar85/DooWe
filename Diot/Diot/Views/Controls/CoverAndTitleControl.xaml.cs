using Diot.Models;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Diot.Views.Controls
{
    /// <summary>
    ///     Control that displays the cover image 
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentView" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CoverAndTitleControl
	{
        #region Fields

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the tapped command.
        /// </summary>
        public ICommand TappedCommand
	    {
	        get => (Command) GetValue(TappedCommandProperty);
	        set => SetValue(TappedCommandProperty, value);
	    }

        /// <summary>
        ///     Gets or sets the tapped command parameter.
        /// </summary>
        public MovieDbModel CommandParameter
        {
	        get => (MovieDbModel) GetValue(CommandParameterProperty);
	        set => SetValue(CommandParameterProperty, value);
	    }

        /// <summary>
        ///     Gets or sets the cover image.
        /// </summary>
        public ImageSource CoverImage
	    {
	        get => (ImageSource) GetValue(CoverImageProperty);
	        set => SetValue(CoverImageProperty, value);
	    }

        /// <summary>
        ///     Gets or sets the title.
        /// </summary>
        public string Title
	    {
	        get => (string) GetValue(TitleProperty);
	        set => SetValue(TitleProperty, value);
	    }

        #endregion

        #region Methods

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CoverAndTitleControl"/> class.
        /// </summary>
        public CoverAndTitleControl ()
		{
			InitializeComponent ();
		}

        #endregion

        #endregion

        #region Bindable Properties

	    /// <summary>
	    ///     The bindable property for <see cref="Title"/>.
	    /// </summary>
	    public static readonly BindableProperty TitleProperty =
	        BindableProperty.Create(
	            nameof(Title),
	            typeof(string),
	            typeof(CoverAndTitleControl),
	            default(string),
	            propertyChanged: (bindable, oldValue, newValue) =>
	                ((CoverAndTitleControl)bindable).updateTitle());

        /// <summary>
        ///     The bindable property for <see cref="CoverImage"/>.
        /// </summary>
        public static readonly BindableProperty CoverImageProperty =
	        BindableProperty.Create(
	            nameof(CoverImage),
	            typeof(ImageSource),
	            typeof(CoverAndTitleControl),
	            default(ImageSource),
	            propertyChanged: (bindable, oldValue, newValue) =>
	                ((CoverAndTitleControl)bindable).updateCoverImage());

        /// <summary>
        ///     The bindable property for <see cref="TappedCommand"/>.
        /// </summary>
        public static readonly BindableProperty TappedCommandProperty =
            BindableProperty.Create(
                nameof(TappedCommand), 
                typeof(ICommand), 
                typeof(CoverAndTitleControl),
                default(ICommand),
                propertyChanged: (bindable, oldValue, newValue) =>
                    ((CoverAndTitleControl)bindable).updateTappedCommand());

        /// <summary>
        ///     The bindable property for <see cref="CommandParameter"/>.
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(
                nameof(CommandParameter),
                typeof(MovieDbModel),
                typeof(CoverAndTitleControl),
                null,
                propertyChanged: (bindable, oldValue, newValue) =>
                    ((CoverAndTitleControl)bindable).updateTappedCommandParameterProperty());

        #endregion

        #region Methods

        /// <summary>
        ///     Updates the title.
        /// </summary>
        private void updateTitle()
        {
            TitleLbl.Text = Title;
        }

        /// <summary>
        ///     Updates the cover image.
        /// </summary>
        private void updateCoverImage()
        {
            CoverImg.Source = CoverImage;
            CoverImg.IsVisible = true;
            NoCoverImageControl.IsVisible = false;
        }

        /// <summary>
        ///     Updates the tapped command parameter property.
        /// </summary>
        private void updateTappedCommandParameterProperty()
	    {
	        TapGestureRecognizer.CommandParameter = CommandParameter;
	    }

        /// <summary>
        ///     Updates the tap gesture recognizer command.
        /// </summary>
        private void updateTappedCommand()
	    {
	        TapGestureRecognizer.Command = TappedCommand;
	    }

        #endregion
    }
}