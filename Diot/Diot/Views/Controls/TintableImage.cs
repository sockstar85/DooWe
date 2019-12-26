using Xamarin.Forms;

namespace Diot.Views.Controls
{
	public class TintableImage : Image
	{
		#region Properties

		/// <summary>
		///		Gets or sets the color of the tint.
		/// </summary>
		public Color TintColor
		{
			get { return (Color)GetValue(TintColorProperty); }
			set { SetValue(TintColorProperty, value); }
		}

		#endregion

		#region Bindable Properties

		/// <summary>
		///		The bindable property for <see cref="TintColor"/>.
		/// </summary>
		public static readonly BindableProperty TintColorProperty = 
			BindableProperty.Create(
				nameof(TintColor), 
				typeof(Color), 
				typeof(TintableImage), 
				Color.Transparent);

		#endregion
	}
}
