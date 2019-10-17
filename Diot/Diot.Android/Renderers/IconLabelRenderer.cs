using Android.Content;
using Android.Graphics;
using Diot.Droid.Renderers;
using Diot.Helpers;
using Diot.Views.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(IconLabel), typeof(IconLabelRenderer))]
namespace Diot.Droid.Renderers
{
    /// <summary>
    ///     Renderer for creating the iconfont on Android.
    /// </summary>
    /// <seealso cref="LabelRenderer" />
    public class IconLabelRenderer : LabelRenderer
    {
        #region Fields

        private readonly Context _context;

        #endregion

        #region Methods

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="IconLabelRenderer"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public IconLabelRenderer(Context context) : base(context)
        {
            _context = context;
        }

        #endregion

        /// <summary>
        ///     Raises the <see cref="E:ElementChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="ElementChangedEventArgs{Label}"/> instance containing the event data.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            var font = Typeface.CreateFromAsset(_context.ApplicationContext.Assets, AppConsts.IconsFontFile);
            Control.Typeface = font;
        }

        #endregion
    }
}