using Diot.iOS.Effects.Entries;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportEffect(typeof(EntryNoUnderlineEffect), nameof(EntryNoUnderlineEffect))]
namespace Diot.iOS.Effects.Entries
{
	public class EntryNoUnderlineEffect : PlatformEffect
	{
		#region Methods

		/// <summary>
		///		Method that is called after the effect is attached and made valid.
		/// </summary>
		protected override void OnAttached()
		{
			if (Control is UITextField uiTextField)
			{
				uiTextField.BorderStyle = UITextBorderStyle.None;
			}
		}

		/// <summary>
		///		Method that is called after the effect is detached and invalidated.
		/// </summary>
		protected override void OnDetached()
		{
			if (Control is UITextField uiTextField)
			{
				uiTextField.BorderStyle = default(UITextBorderStyle);
			}
		}

		#endregion
	}
}