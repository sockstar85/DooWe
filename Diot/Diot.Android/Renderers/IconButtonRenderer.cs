using Android.Content;
using Android.Graphics;
using Diot.Droid.Renderers;
using Diot.Helpers;
using Diot.Views.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(IconButton), typeof(IconButtonRenderer))]
namespace Diot.Droid.Renderers
{
	public class IconButtonRenderer : ButtonRenderer
	{
		#region Fields
		
		private Context _context;

		#endregion

		#region Methods

		#region Constructors

		/// <summary>
		///		Initializes a new instance of the <see cref="IconButtonRenderer"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public IconButtonRenderer(Context context) : base(context)
		{
			_context = context;
		}

		#endregion

		/// <summary>
		///		Raises the <see cref="E:ElementChanged" /> event.
		/// </summary>
		/// <param name="e">The <see cref="ElementChangedEventArgs{Button}"/> instance containing the event data.</param>
		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged(e);

			var font = Typeface.CreateFromAsset(_context.ApplicationContext.Assets, AppConsts.IconsFontFile);
			Control.Typeface = font;
		}

		#endregion
	}
}