using Xamarin.Forms;

namespace Diot.Views.Controls
{
	public class IconButton : Button
	{
		#region Properties

		/// <summary>
		///     Gets or sets the icon code.
		/// </summary>
		public string IconCode
		{
			get => (string)GetValue(IconCodeProperty);
			set => SetValue(IconCodeProperty, value);
		}

		/// <summary>
		///     Gets or sets the size of the icon.
		/// </summary>
		public double IconSize
		{
			get => (double)GetValue(IconSizeProperty);
			set => SetValue(IconSizeProperty, value);
		}

		/// <summary>
		///     Gets or sets the color of the icon.
		/// </summary>
		public Color IconColor
		{
			get => (Color)GetValue(IconColorProperty);
			set => SetValue(IconColorProperty, value);
		}

		#endregion


		#region Bindable Properties

		/// <summary>
		///     The bindable property for <see cref="IconCode"/>.
		/// </summary>
		public static readonly BindableProperty IconCodeProperty =
			BindableProperty.Create(
				nameof(IconCode),
				typeof(string),
				typeof(IconButton),
				default(string),
				propertyChanged:
				(bindable, oldValue, newValue) =>
					((IconButton)bindable).updateCode());

		/// <summary>
		///     The bindable property for <see cref="IconSize"/>.
		/// </summary>
		public static readonly BindableProperty IconSizeProperty =
			BindableProperty.Create(
				nameof(IconSize),
				typeof(double),
				typeof(IconButton),
				default(double),
				propertyChanged:
				(bindable, oldValue, newValue) =>
					((IconButton)bindable).updateSize());

		/// <summary>
		///     The bindable property for <see cref="IconColor"/>.
		/// </summary>
		public static readonly BindableProperty IconColorProperty =
			BindableProperty.Create(
				nameof(IconColor),
				typeof(Color),
				typeof(IconButton),
				default(Color),
				propertyChanged:
				(bindable, oldValue, newValue) =>
					((IconButton)bindable).updateColor());

		#endregion

		#region Methods

		/// <summary>
		///     Updates the code.
		/// </summary>
		private void updateCode()
		{
			Text = IconCode;
		}

		/// <summary>
		///     Updates the size.
		/// </summary>
		private void updateSize()
		{
			FontSize = IconSize;
		}

		/// <summary>
		///     Updates the color.
		/// </summary>
		private void updateColor()
		{
			TextColor = IconColor;
		}

		#endregion
	}
}
