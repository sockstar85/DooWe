using Diot.Droid.Effects.Entries;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportEffect(typeof(EntryNoUnderlineEffect), nameof(EntryNoUnderlineEffect))]
namespace Diot.Droid.Effects.Entries
{
	public class EntryNoUnderlineEffect : PlatformEffect
	{
		#region Methods

		/// <summary>
		///		Method that is called after the effect is attached and made valid.
		/// </summary>
		protected override void OnAttached()
		{
			Control.SetBackgroundColor(Color.Transparent.ToAndroid());
		}

		/// <summary>
		///		Method that is called after the effect is detached and invalidated.
		/// </summary>
		protected override void OnDetached()
		{
			Control.SetBackgroundColor(default(Color).ToAndroid());
		}

		#endregion
	}
}