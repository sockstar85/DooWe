using Diot.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Diot.Views.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ActionButtonControl
	{
		#region Fields

		#endregion

		#region Properties

		/// <summary>
		///		Gets or sets the width and height of the control equally.
		/// </summary>
		public double Size
		{
			get => (double)GetValue(SizeProperty);
			set => SetValue(SizeProperty, value);
		}

		/// <summary>
		///		NOTE: This property is ignored. Used <see cref="Size"/> to set the width and height equally.
		/// </summary>
		public new double WidthRequest
		{
			get; set;
		}

		/// <summary>
		///		NOTE: This property is ignored. Used <see cref="Size"/> to set the width and height equally.
		/// </summary>
		public new double HeightRequest
		{
			get; set;
		}

		/// <summary>
		///		Gets or sets the color which will fill the background of a VisualElement. This is a bindable property.
		/// </summary>
		public new Color BackgroundColor
		{
			get => (Color)GetValue(BackgroundColorProperty);
			set => SetValue(BackgroundColorProperty, value);
		}

		/// <summary>
		///		Gets or sets the icon code.
		/// </summary>
		public string IconCode
		{
			get => (string)GetValue(IconCodeProperty);
			set => SetValue(IconCodeProperty, value);
		}

		#endregion

		#region Bindable Properties

		/// <summary>
		///		The bindable property for <see cref="Size"/>.
		/// </summary>
		public BindableProperty SizeProperty =
			BindableProperty.Create(
				nameof(Size),
				typeof(double),
				typeof(ActionButtonControl),
				-1D,
				propertyChanged:
					(bindable, oldValue, newValue) =>
						((ActionButtonControl)bindable).updateSize());

		/// <summary>
		///		The bindable property for <see cref="BackgroundColor"/>.
		/// </summary>
		public new BindableProperty BackgroundColorProperty =
			BindableProperty.Create(
				nameof(BackgroundColor),
				typeof(Color),
				typeof(ActionButtonControl),
				Color.Default,
				propertyChanged:
					(bindable, oldValue, newValue) =>
						((ActionButtonControl)bindable).updateBackgroundColor());

		/// <summary>
		///		The bindable proeprty for <see cref="IconCode"/>.
		/// </summary>
		public BindableProperty IconCodeProperty =
			BindableProperty.Create(
				nameof(IconCode),
				typeof(string),
				typeof(ActionButtonControl),
				AppConsts.IconFontCodes.ico_add,
				propertyChanged:
					(bindable, oldValue, newValue) =>
						((ActionButtonControl)bindable).updateIconCode());

		#endregion

		#region Methods

		#region Constructors

		/// <summary>
		///		Initializes a new instance of the <see cref="ActionButtonControl"/> class.
		/// </summary>
		public ActionButtonControl()
		{
			InitializeComponent();
		}

		#endregion

		/// <summary>
		///		Updates the size.
		/// </summary>
		private void updateSize()
		{
			OuterContainer.WidthRequest = Size;
			CircularBackground.CornerRadius = new CornerRadius(Size / 2);
		}

		/// <summary>
		///		Updates the color of the background circle of the control.
		/// </summary>
		private void updateBackgroundColor()
		{
			CircularBackground.BackgroundColor = BackgroundColor;
		}

		/// <summary>
		///		Updates the icon code.
		/// </summary>
		private void updateIconCode()
		{
			Icon.IconCode = IconCode;
		}

		#endregion
	}
}