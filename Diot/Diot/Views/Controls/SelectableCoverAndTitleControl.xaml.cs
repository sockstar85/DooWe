using System;
using System.Windows.Input;
using Diot.Interface;
using Diot.Interface.ViewModels;
using Diot.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Diot.ViewModels;

namespace Diot.Views.Controls
{
    /// <summary>
    ///     Control that displays the cover image 
    /// </summary>
    /// <seealso cref="ContentView" />
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelectableCoverAndTitleControl
	{
        #region Fields

        private IResourceManager _resourceManager => ResourceManager.Instance;
	    private ISelectableMovieViewModel _movieObject;

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
        public ISelectableMovieViewModel CommandParameter
        {
	        get => (SelectableMovieViewModel) GetValue(CommandParameterProperty);
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
        ///     Initializes a new instance of the <see cref="SelectableCoverAndTitleControl"/> class.
        /// </summary>
        public SelectableCoverAndTitleControl ()
		{
			InitializeComponent ();
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
                typeof(SelectableCoverAndTitleControl),
	            default(Color),
	            propertyChanged: (bindable, oldValue, newValue) => 
	                ((SelectableCoverAndTitleControl)bindable).updateIsSelected());

        #region Bindable Properties

        /// <summary>
        ///     The bindable property for <see cref="IsSelected"/>.
        /// </summary>
        public static readonly BindableProperty IsSelectedProperty =
            BindableProperty.Create(
                nameof(IsSelected),
                typeof(bool),
                typeof(SelectableCoverAndTitleControl),
                default(bool),
                propertyChanged: (bindable, oldValue, newValue) =>
                    ((SelectableCoverAndTitleControl)bindable).updateIsSelected());
        
        /// <summary>
        ///     The bindable property for <see cref="Title"/>.
        /// </summary>
        public static readonly BindableProperty TitleProperty =
	        BindableProperty.Create(
	            nameof(Title),
	            typeof(string),
	            typeof(SelectableCoverAndTitleControl),
	            default(string),
	            propertyChanged: (bindable, oldValue, newValue) =>
	                ((SelectableCoverAndTitleControl)bindable).updateTitle());

        /// <summary>
        ///     The bindable property for <see cref="CoverImage"/>.
        /// </summary>
        public static readonly BindableProperty CoverImageProperty =
	        BindableProperty.Create(
	            nameof(CoverImage),
	            typeof(ImageSource),
	            typeof(SelectableCoverAndTitleControl),
	            default(ImageSource),
	            propertyChanged: (bindable, oldValue, newValue) =>
	                ((SelectableCoverAndTitleControl)bindable).updateCoverImage());

        /// <summary>
        ///     The bindable property for <see cref="TappedCommand"/>.
        /// </summary>
        public static readonly BindableProperty TappedCommandProperty =
            BindableProperty.Create(
                nameof(TappedCommand), 
                typeof(ICommand), 
                typeof(SelectableCoverAndTitleControl),
                default(ICommand),
                propertyChanged: (bindable, oldValue, newValue) =>
                    ((SelectableCoverAndTitleControl)bindable).updateTappedCommand());

        /// <summary>
        ///     The bindable property for <see cref="CommandParameter"/>.
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(
                nameof(CommandParameter),
                typeof(SelectableMovieViewModel),
                typeof(SelectableCoverAndTitleControl),
                null,
                propertyChanged: (bindable, oldValue, newValue) =>
                    ((SelectableCoverAndTitleControl)bindable).updateTappedCommandParameterProperty());

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
	        _movieObject = CommandParameter;

	        if (_movieObject?.Movie?.CoverImageByteArray == null || _movieObject.Movie.CoverImageByteArray.Length == 0)
	        {
	            Console.WriteLine("Well, I think we have an empty image");
	        }
        }

        /// <summary>
        ///     Updates the tap gesture recognizer command.
        /// </summary>
        private void updateTappedCommand()
	    {
	        TapGestureRecognizer.Command = TappedCommand;
            updateCoverImage();
	    }

        /// <summary>
        ///     Updates selected border.
        /// </summary>
        private void updateIsSelected()
        {
            SelectedIconSolid.IsVisible = IsSelected;
            SelectedIcon.IsVisible = IsSelected;
        }

        #endregion
    }
}