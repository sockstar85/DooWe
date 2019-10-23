using System;
using Diot.Models;
using System.Windows.Input;
using Diot.Interface;
using Diot.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;

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

        IResourceManager _resourceManager => ResourceManager.Instance;
	    private readonly double _coverImgScale;

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

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        public bool IsSelected
	    {
	        get => (bool) GetValue(IsSelectedProperty);
	        set => SetValue(IsSelectedProperty, value);
	    }

        /// <summary>
        ///     Gets or sets the color which will fill the background of a VisualElement.
        /// </summary>
        public new Color BackgroundColor
	    {
	        get => (Color) GetValue(BackgroundColorProperty);
	        set => SetValue(BackgroundColorProperty, value);
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
		    _coverImgScale = CoverImg.Scale;
		}

        #endregion

        #endregion

        /// <summary>
        ///     The bindable property for <see cref="BackgroundProperty"/>.
        /// </summary>
        public new static readonly BindableProperty BackgroundColorProperty =
	        BindableProperty.Create(
	            nameof(BackgroundColor),
	            typeof(Color),
                typeof(CoverAndTitleControl),
	            default(Color),
	            propertyChanged: (bindable, oldValue, newValue) => 
	                ((CoverAndTitleControl)bindable).updateIsSelected());

        #region Bindable Properties

        /// <summary>
        ///     The bindable property for <see cref="IsSelected"/>.
        /// </summary>
        public static readonly BindableProperty IsSelectedProperty =
            BindableProperty.Create(
                nameof(IsSelected),
                typeof(bool),
                typeof(CoverAndTitleControl),
                default(bool),
                propertyChanged: (bindable, oldValue, newValue) =>
                    ((CoverAndTitleControl)bindable).updateIsSelected());
        
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

        /// <summary>
        ///     Updates selected border.
        /// </summary>
        private void updateIsSelected()
        {
            SelectedIcon.IsVisible = IsSelected;
        }

        #endregion

        #region Events

        /// <summary>
        ///     Handles the OnTapped event of the TapGestureRecognizer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
	    {
	        if (!IsEnabled)
	        {
	            return;
	        }

	        uint length = 200;

	        Task.Run(async () =>
	        {

#pragma warning disable CS4014 //Disabling for animations

	            CoverImg.FadeTo(0.75, length / 2);
	            await CoverImg.ScaleTo(_coverImgScale * 0.90, length / 2, Easing.CubicOut);
	            CoverImg.FadeTo(1, length);
	            CoverImg.ScaleTo(_coverImgScale, length, Easing.SpringOut);

#pragma warning restore CS4014

	        });
        }

        #endregion
    }
}