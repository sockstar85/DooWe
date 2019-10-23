using Diot.Helpers;
using Diot.iOS.Renderers;
using Diot.Views.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(IconLabel), typeof(IconLabelRenderer))]
namespace Diot.iOS.Renderers
{
    /// <summary>
    ///     Renderer for the <see cref="IconLabel"/> on iOS.
    /// </summary>
    /// <seealso cref="LabelRenderer" />
    public class IconLabelRenderer : LabelRenderer
    {
        #region Methods

        /// <summary>
        ///     Called when element property changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected override void OnElementPropertyChanged(object sender,
            System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals("Text"))
            {
                var label = sender as Label;
                var font = UIFont.FromName(AppConsts.IconsFontFamilyTitle, (int)label.FontSize);

                Control.Font = font;
            }
        }

        /// <summary>
        ///     Raises the <see cref="E:ElementChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="ElementChangedEventArgs{Label}"/> instance containing the event data.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            var fontSize = e.NewElement?.FontSize;
            if (fontSize != null)
            {
                var font = UIFont.FromName(AppConsts.IconsFontFamilyTitle, (int)fontSize);
                Control.Font = font;
            }
        }

        #endregion
    }
}