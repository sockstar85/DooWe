using Diot.Views.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(TintableImage), typeof(Diot.iOS.Renderers.TintableImage))]
namespace Diot.iOS.Renderers
{
	public class TintableImage : ImageRenderer
	{
		/// <summary>
		///		Raises the <see cref="E:ElementChanged" /> event.
		/// </summary>
		/// <param name="e">The <see cref="ElementChangedEventArgs{Image}"/> instance containing the event data.</param>
		protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
		{
			base.OnElementChanged(e);
			SetTint();
		}

		/// <summary>
		///		Called when element property changed.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == Views.Controls.TintableImage.TintColorProperty.PropertyName ||
				e.PropertyName == Image.SourceProperty.PropertyName)
			{
				SetTint();
			}
		}

		/// <summary>
		///		Sets the tint.
		/// </summary>
		void SetTint()
		{
			if (Control?.Image == null || Element == null)
			{
				return;
			}

			if (((Views.Controls.TintableImage)Element).TintColor == Color.Transparent)
			{
				//Turn off tinting
				Control.Image = Control.Image.ImageWithRenderingMode(UIKit.UIImageRenderingMode.Automatic);
				Control.TintColor = null;
			}
			else
			{
				//Apply tint color
				Control.Image = Control.Image.ImageWithRenderingMode(UIKit.UIImageRenderingMode.AlwaysTemplate);
				Control.TintColor = ((Views.Controls.TintableImage)Element).TintColor.ToUIColor();
			}
		}
	}
}