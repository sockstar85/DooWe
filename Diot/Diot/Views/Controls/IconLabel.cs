using Xamarin.Forms;

namespace Diot.Views.Controls
{
    /// <summary>
    ///     Control that extends <see cref="Label"/> and adds properties that make more sense for setting an icon.
    /// </summary>
    /// <seealso cref="Xamarin.Forms.Label" />
    public class IconLabel : Label
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the icon code.
        /// </summary>
        public string IconCode
        {
            get => (string) GetValue(IconCodeProperty);
            set => SetValue(IconCodeProperty, value);
        }

        /// <summary>
        ///     Gets or sets the size of the icon.
        /// </summary>
        public double IconSize
        {
            get => (double) GetValue(IconSizeProperty);
            set => SetValue(IconSizeProperty, value);
        }

        /// <summary>
        ///     Gets or sets the color of the icon.
        /// </summary>
        public Color IconColor
        {
            get => (Color) GetValue(IconColorProperty);
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
                typeof(IconLabel),
                default(string),
                propertyChanged:
                (bindable, oldValue, newValue) =>
                    ((IconLabel)bindable).updateCode());

        /// <summary>
        ///     The bindable property for <see cref="IconSize"/>.
        /// </summary>
        public static readonly BindableProperty IconSizeProperty =
            BindableProperty.Create(
                nameof(IconSize), 
                typeof(double),
                typeof(IconLabel),
                default(double),
                propertyChanged:
                (bindable, oldValue, newValue) =>
                    ((IconLabel)bindable).updateSize());

        /// <summary>
        ///     The bindable property for <see cref="IconColor"/>.
        /// </summary>
        public static readonly BindableProperty IconColorProperty =
            BindableProperty.Create(
                nameof(IconColor), 
                typeof(Color), 
                typeof(IconLabel),
                default(Color),
                propertyChanged:
                (bindable, oldValue, newValue) =>
                    ((IconLabel)bindable).updateColor());

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
