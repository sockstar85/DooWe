using System;
using Diot.Interface;
using Diot.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Diot.Views.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BorderedEntry
	{
		#region Fields

		static IResourceManager _resourceManager = ResourceManager.Instance;

		#endregion

		#region Properties

		/// <summary>
		///		Gets or sets the horizontal text alignment.
		/// </summary>
		public TextAlignment HorizontalTextAlignment
		{
			get => (TextAlignment)GetValue(HorizontalTextAlignmentProperty);
			set => SetValue(HorizontalTextAlignmentProperty, value);
		}
		
		/// <summary>
		///		Gets or set is password.
		/// </summary>
		public bool IsPassword
		{
			get => (bool)GetValue(IsPasswordProperty);
			set => SetValue(IsPasswordProperty, value);
		}

		/// <summary>
		///		Gets or sets placeholder.
		/// </summary>
		public string Placeholder
		{
			get => (string)GetValue(PlaceholderProperty);
			set => SetValue(PlaceholderProperty, value);
		}

		/// <summary>
		///		Gets or sets placeholder color.
		/// </summary>
		public Color PlaceholderColor
		{
			get => (Color)GetValue(PlaceholderColorProperty);
			set => SetValue(PlaceholderColorProperty, value);
		}

		/// <summary>
		///		Gets or sets text.
		/// </summary>
		public string Text
		{
			get => (string)GetValue(TextProperty);
			set => SetValue(TextProperty, value);
		}

		/// <summary>
		///		Gets or sets text color.
		/// </summary>
		public Color TextColor
		{
			get => (Color)GetValue(TextColorProperty);
			set => SetValue(TextColorProperty, value);
		}

		/// <summary>
		///		Gets or sets font attributes.
		/// </summary>
		public FontAttributes FontAttributes
		{
			get => (FontAttributes)GetValue(FontAttributesProperty);
			set => SetValue(FontAttributesProperty, value);
		}

		/// <summary>
		///		Gets or sets font family.
		/// </summary>
		public string FontFamily
		{
			get => (string)GetValue(FontFamilyProperty);
			set => SetValue(FontFamilyProperty, value);
		}

		/// <summary>
		///		Gets or sets font size.
		/// </summary>
		public double FontSize
		{
			get => (double)GetValue(FontSizeProperty);
			set => SetValue(FontSizeProperty, value);
		}

		/// <summary>
		///		Gets or sets is text predition enabled.
		/// </summary>
		public bool IsTextPredictionEnabled
		{
			get => (bool)GetValue(IsTextPredictionEnabledProperty);
			set => SetValue(IsTextPredictionEnabledProperty, value);
		}


		/// <summary>
		///		Gets or sets the color of the border.
		/// </summary>
		public Color BorderColor
		{
			get => (Color)GetValue(BorderColorProperty);
			set => SetValue(BorderColorProperty, value);
		}

		/// <summary>
		///		Gets or sets the corner radius.
		/// </summary>
		public CornerRadius CornerRadius
		{
			get => (CornerRadius)GetValue(CornerRadiusProperty);
			set => SetValue(CornerRadiusProperty, value);
		}

		/// <summary>
		///		Gets or sets the keyboard.
		/// </summary>
		public Keyboard Keyboard
		{
			get => (Keyboard)GetValue(KeyboardProperty);
			set => SetValue(KeyboardProperty, value);
		}

		/// <summary>
		///		Gets or sets the width of the border.
		/// </summary>
		public double BorderWidth
		{
			get => (double)GetValue(BorderWidthProperty);
			set => SetValue(BorderWidthProperty, value);
		}

		#endregion

		#region Bindable Properties

		/// <summary>
		///		The bindable property for <see cref="BorderWidth"/>.
		/// </summary>
		public static readonly BindableProperty BorderWidthProperty =
				BindableProperty.Create(
					nameof(BorderWidth),
					typeof(double),
					typeof(BorderedEntry),
					1.0D,
					propertyChanged: (bindable, oldValue, newValue) =>
						((BorderedEntry)bindable).updateBorderWidth());

		/// <summary>
		///		The bindable property for <see cref="BorderColor"/>.
		/// </summary>
		public static readonly BindableProperty BorderColorProperty =
				BindableProperty.Create(
					nameof(BorderColor),
					typeof(Color),
					typeof(BorderedEntry),
					(Color)_resourceManager.GetResource("PrimaryColor"),
					propertyChanged: (bindable, oldValue, newValue) =>
						((BorderedEntry)bindable).updateBorderColor());

		/// <summary>
		///		The bindable property for <see cref="CornerRadius"/>.
		/// </summary>
		public static readonly BindableProperty CornerRadiusProperty =
				BindableProperty.Create(
					nameof(CornerRadius),
					typeof(CornerRadius),
					typeof(BorderedEntry),
					BoxView.CornerRadiusProperty.DefaultValue,
					propertyChanged: (bindable, oldValue, newValue) =>
						((BorderedEntry)bindable).updateCornerRadius());

		/// <summary>
		///		The bindable property for <see cref="Keyboard"/>.
		/// </summary>
		public static readonly BindableProperty KeyboardProperty =
				BindableProperty.Create(
					nameof(Keyboard),
					typeof(Keyboard),
					typeof(BorderedEntry),
					Entry.KeyboardProperty.DefaultValue,
					propertyChanged: (bindable, oldValue, newValue) =>
						((BorderedEntry)bindable).updateKeyboard());

		 /// <summary>
		 ///     The bindable property for <see cref="HorizontalTextAlignment"/>.
		 /// </summary>
		 public static readonly BindableProperty HorizontalTextAlignmentProperty =
			BindableProperty.Create(
					nameof(HorizontalTextAlignment),
					typeof(TextAlignment),
					typeof(BorderedEntry),
					Entry.HorizontalTextAlignmentProperty.DefaultValue,
					propertyChanged: (bindable, oldValue, newValue) =>
						((BorderedEntry)bindable).updateHorizontalTextAlignment());
		
		/// <summary>
		///     The bindable property for <see cref="IsPassword"/>.
		/// </summary>
		public static readonly BindableProperty IsPasswordProperty =
			BindableProperty.Create(
				nameof(IsPassword),
				typeof(bool),
				typeof(BorderedEntry),
				Entry.IsPasswordProperty.DefaultValue,
				propertyChanged: (bindable, oldValue, newValue) =>
					((BorderedEntry)bindable).updateIsPassword());
		
		/// <summary>
		///     The bindable property for <see cref="Placeholder"/>.
		/// </summary>
		public static readonly BindableProperty PlaceholderProperty =
			BindableProperty.Create(
				nameof(Placeholder),
				typeof(string),
				typeof(BorderedEntry),
				Entry.PlaceholderProperty.DefaultValue,
				propertyChanged: (bindable, oldValue, newValue) =>
					((BorderedEntry)bindable).updatePlaceholder());

		/// <summary>
		///     The bindable property for <see cref="PlaceholderColor"/>.
		/// </summary>
		public static readonly BindableProperty PlaceholderColorProperty =
			BindableProperty.Create(
				nameof(PlaceholderColor),
				typeof(Color),
				typeof(BorderedEntry),
				Entry.PlaceholderColorProperty.DefaultValue,
				propertyChanged: (bindable, oldValue, newValue) =>
					((BorderedEntry)bindable).updatePlaceholderColor());

		/// <summary>
		///     The bindable property for <see cref="Text"/>.
		/// </summary>
		public static readonly BindableProperty TextProperty =
			BindableProperty.Create(
				nameof(Text),
				typeof(string),
				typeof(BorderedEntry),
				Entry.TextProperty.DefaultValue,
				propertyChanged: (bindable, oldValue, newValue) =>
					((BorderedEntry)bindable).updateText(), 
				defaultBindingMode: BindingMode.TwoWay);

		/// <summary>
		///     The bindable property for <see cref="TextColor"/>.
		/// </summary>
		public static readonly BindableProperty TextColorProperty =
			BindableProperty.Create(
				nameof(TextColor),
				typeof(Color),
				typeof(BorderedEntry),
				Entry.TextColorProperty.DefaultValue,
				propertyChanged: (bindable, oldValue, newValue) =>
					((BorderedEntry)bindable).updateTextColor());

		/// <summary>
		///     The bindable property for <see cref="FontAttributes"/>.
		/// </summary>
		public static readonly BindableProperty FontAttributesProperty =
			BindableProperty.Create(
				nameof(FontAttributes),
				typeof(FontAttributes),
				typeof(BorderedEntry),
				Entry.FontAttributesProperty.DefaultValue,
				propertyChanged: (bindable, oldValue, newValue) =>
					((BorderedEntry)bindable).updateFontAttributes());

		/// <summary>
		///     The bindable property for <see cref="FontFamily"/>.
		/// </summary>
		public static readonly BindableProperty FontFamilyProperty =
			BindableProperty.Create(
				nameof(FontFamily),
				typeof(string),
				typeof(BorderedEntry),
				Entry.FontFamilyProperty.DefaultValue,
				propertyChanged: (bindable, oldValue, newValue) =>
					((BorderedEntry)bindable).updateFontFamily());

		/// <summary>
		///     The bindable property for <see cref="FontSize"/>.
		/// </summary>
		public static readonly BindableProperty FontSizeProperty =
			BindableProperty.Create(
				nameof(FontSize),
				typeof(double),
				typeof(BorderedEntry),
				Entry.FontSizeProperty.DefaultValue,
				propertyChanged: (bindable, oldValue, newValue) =>
					((BorderedEntry)bindable).updateFontSize());

		/// <summary>
		///     The bindable property for <see cref="IsTextPredictionEnabled"/>.
		/// </summary>
		public static readonly BindableProperty IsTextPredictionEnabledProperty =
			BindableProperty.Create(
				nameof(IsTextPredictionEnabled),
				typeof(bool),
				typeof(BorderedEntry),
				Entry.IsTextPredictionEnabledProperty.DefaultValue,
				propertyChanged: (bindable, oldValue, newValue) =>
					((BorderedEntry)bindable).updateIsTextPredictionEnabled());

		#endregion

		#region Methods

		#region Constructors

		/// <summary>
		///		Initializes a new instance of the <see cref="BorderedEntry"/> class.
		/// </summary>
		public BorderedEntry()
		{
			InitializeComponent();
			ControlEntry.TextChanged += controlEntryTextChanged;
		}

		#endregion

		/// <summary>
		///		Updates the color of the border.
		/// </summary>
		/// <exception cref="NotImplementedException"></exception>
		private void updateBorderColor()
		{
			OuterBoxView.BackgroundColor = BorderColor;
		}
			   
		/// <summary>
		///		Updates the corner radius.
		/// </summary>
		private void updateCornerRadius()
		{
			OuterBoxView.CornerRadius = CornerRadius;

			var innerOffset = 0.25;

			//This helps to ensure the inner corner radius is a little less than the outer to look like a real stroke.
			InnerBoxView.CornerRadius = new CornerRadius(
				CornerRadius.BottomLeft > 0 ? CornerRadius.BottomLeft * innerOffset : 0,
				CornerRadius.TopLeft > 0 ? CornerRadius.TopLeft * innerOffset : 0,
				CornerRadius.TopRight > 0 ? CornerRadius.TopRight * innerOffset : 0,
				CornerRadius.BottomRight > 0 ? CornerRadius.BottomRight * innerOffset : 0);
		}

		/// <summary>
		///		Updates the font family.
		/// </summary>
		private void updateFontFamily()
		{
			ControlEntry.FontFamily = FontFamily;
		}

		/// <summary>
		/// Updates the horizontal text alignment.
		/// </summary>
		private void updateHorizontalTextAlignment()
		{
			ControlEntry.HorizontalTextAlignment = HorizontalTextAlignment;
		}

		/// <summary>
		/// Updates the is password.
		/// </summary>
		private void updateIsPassword()
		{
			ControlEntry.IsPassword = IsPassword;
		}

		/// <summary>
		/// Updates the placeholder.
		/// </summary>
		private void updatePlaceholder()
		{
			ControlEntry.Placeholder = Placeholder;
		}

		/// <summary>
		/// Updates the color of the placeholder.
		/// </summary>
		private void updatePlaceholderColor()
		{
			ControlEntry.PlaceholderColor = PlaceholderColor;
		}

		/// <summary>
		/// Updates the text.
		/// </summary>
		private void updateText()
		{
			ControlEntry.Text = Text;
		}

		/// <summary>
		/// Updates the color of the text.
		/// </summary>
		private void updateTextColor()
		{
			ControlEntry.TextColor = TextColor;
		}
		
		/// <summary>
		///		Updates the font attributes.
		/// </summary>
		private void updateFontAttributes()
		{
			ControlEntry.FontAttributes = FontAttributes;
		}

		/// <summary>
		///		Updates the size of the font.
		/// </summary>
		private void updateFontSize()
		{
			ControlEntry.FontSize = FontSize;
		}

		/// <summary>
		/// Updates the is text prediction enabled.
		/// </summary>
		private void updateIsTextPredictionEnabled()
		{
			ControlEntry.IsTextPredictionEnabled = IsTextPredictionEnabled;
		}

		/// <summary>
		///		Updates the keyboard.
		/// </summary>
		private void updateKeyboard()
		{
			ControlEntry.Keyboard = Keyboard;
		}

		/// <summary>
		///		Updates the width of the border.
		/// </summary>
		private void updateBorderWidth()
		{
			InnerBoxView.Margin = new Thickness(BorderWidth);
		}

		/// <summary>
		///		Called when the text in the entry changes.
		/// </summary>
		private void controlEntryTextChanged(object sender, TextChangedEventArgs e)
		{
			Text = e.NewTextValue;
			TextChanged?.Invoke(sender, e);
		}

		/// <summary>
		///		Overrides the base to sync up to the InputTransparent
		///		properties between this control and the child control.
		/// </summary>
		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			ControlEntry.InputTransparent = InputTransparent;
		}

		#endregion

		#region Events

		/// <summary>
		///		Occurs when text changed.
		/// </summary>
		public event EventHandler<TextChangedEventArgs> TextChanged;

		#endregion
	}
}