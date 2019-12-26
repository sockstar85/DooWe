using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using Diot.Views.Controls;
using Android.Graphics;

[assembly:ExportRenderer(typeof(TintableImage), typeof(Diot.Droid.Renderers.TintableImage))]
namespace Diot.Droid.Renderers
{
	public class TintableImage : ImageRenderer
	{
		#region Methods

		#region Constructors

		/// <summary>
		///		Initializes a new instance of the <see cref="TintableImage"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public TintableImage(Context context) : base(context)
		{ 
		}

		#endregion

		/// <summary>
		///		Sets the tint.
		/// </summary>
		void SetTint()
		{
			if (Control == null || Element == null)
			{
				return;
			}

			if (((Views.Controls.TintableImage)Element).TintColor.Equals(Xamarin.Forms.Color.Transparent))
			{
				//Turn off tinting

				if (Control.ColorFilter != null)
					Control.ClearColorFilter();

				return;
			}

			//Apply tint color
			var colorFilter = new PorterDuffColorFilter(((Views.Controls.TintableImage)Element).TintColor.ToAndroid(), PorterDuff.Mode.SrcIn);
			Control.SetColorFilter(colorFilter);
		}
		
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
		/// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == Views.Controls.TintableImage.TintColorProperty.PropertyName ||
				e.PropertyName == Image.SourceProperty.PropertyName)
			{
				SetTint();
			}
		}

		#endregion
	}
}