using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Diot.Views.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchControl
	{
		#region Fields

		#endregion

		#region Properties

		/// <summary>
		///		Gets or sets text.
		/// </summary>
		public string Text
		{
			get => (string)GetValue(TextProperty);
			set => SetValue(TextProperty, value);
		}

		/// <summary>
		///		Gets or sets the placeholder text.
		/// </summary>
		public string Placeholder
		{
			get => (string)GetValue(PlaceholderProperty);
			set => SetValue(PlaceholderProperty, value);
		}

		/// <summary>
		///		Gets or sets the flag indicating whether the control has a search button or not.
		/// </summary>
		public bool HasSearchButton
		{
			get => (bool)GetValue(HasSearchButtonProperty);
			set => SetValue(HasSearchButtonProperty, value);
		}
		
		/// <summary>
		///		Gets or sets the button command.
		/// </summary>
		public ICommand ButtonCommand
		{
			get => (Command)GetValue(ButtonCommandProperty);
			set => SetValue(ButtonCommandProperty, value);
		}

		#endregion

		#region Bindable Properties

		/// <summary>
		///		The bindable property for <see cref="ButtonCommand"/>.
		/// </summary>
		public static readonly BindableProperty ButtonCommandProperty =
			BindableProperty.Create(
				nameof(ButtonCommand),
				typeof(ICommand),
				typeof(SearchControl),
				default(ICommand),
				propertyChanged: (bindable, oldValue, newValue) =>
					((SearchControl)bindable).updateButtonCommand());

		/// <summary>
		///		The bindable property for <see cref="HasSearchButton"/>.
		/// </summary>
		public static readonly BindableProperty HasSearchButtonProperty =
			BindableProperty.Create(
				nameof(HasSearchButton),
				typeof(bool),
				typeof(SearchControl),
				true,
				propertyChanged: (bindable, oldValue, newValue) =>
					((SearchControl)bindable).updateHasSearchButton());

		/// <summary>
		///		The bindable property for <see cref="Text"/>.
		/// </summary>
		public static readonly BindableProperty TextProperty =
			BindableProperty.Create(
				nameof(Text),
				typeof(string),
				typeof(SearchControl),
				Entry.TextProperty.DefaultValue,
				propertyChanged: (bindable, oldValue, newValue) =>
					((SearchControl)bindable).updateText(),
				defaultBindingMode: BindingMode.TwoWay);

		/// <summary>
		///		The bindable property for <see cref="Placeholder"/>.
		/// </summary>
		public static readonly BindableProperty PlaceholderProperty =
			BindableProperty.Create(
				nameof(Placeholder),
				typeof(string),
				typeof(SearchControl),
				Entry.PlaceholderProperty.DefaultValue,
				propertyChanged: (bindable, oldValue, newValue) =>
					((SearchControl)bindable).updatePlaceholderText());

		#endregion

		#region Methods

		#region Constructors

		/// <summary>
		///		Initializes a new instance of the <see cref="SearchControl"/> class.
		/// </summary>
		public SearchControl()
		{
			InitializeComponent();
			ControlEntry.TextChanged += controlEntryTextChanged;
		}

		#endregion
		
		/// <summary>
		///		Updates the button command.
		/// </summary>
		private void updateButtonCommand()
		{
			ButtonGestureRestureRecognizer.Command = ButtonCommand;
		}

		/// <summary>
		///		Updates the search button visibility.
		/// </summary>
		private void updateHasSearchButton()
		{
			ButtonContainer.IsVisible = HasSearchButton;
			ControlIcon.IsVisible = !HasSearchButton;
		}

		/// <summary>
		///		Updates the text.
		/// </summary>
		private void updateText()
		{
			ControlEntry.Text = Text;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void controlEntryTextChanged(object sender, TextChangedEventArgs e)
		{
			Text = e.NewTextValue;
			TextChanged?.Invoke(sender, e);
		}

		/// <summary>
		///		Updates the placeholder text.
		/// </summary>
		private void updatePlaceholderText()
		{
			ControlEntry.Placeholder = Placeholder;
		}

		#endregion

		#region Events

		/// <summary>
		///		Occurs when text changed.
		/// </summary>
		public event EventHandler<TextChangedEventArgs> TextChanged;

		#endregion

		private void ButtonGestureRestureRecognizer_Tapped(object sender, EventArgs e)
		{
			Task.Run(async () =>
			{
				await ButtonContainer.ScaleTo(0.8, 100, Easing.CubicInOut);
				await ButtonContainer.ScaleTo(1.0, 100, Easing.CubicInOut);
			});
		}
	}
}