using Xamarin.Forms;

namespace Diot.Views.Effects.Entry
{
	/// <summary>
	///		This effect removes the underline in entries.
	/// </summary>
	/// <seealso cref="Xamarin.Forms.RoutingEffect" />
	public class EntryNoUnderlineEffect : RoutingEffect
	{
		#region Methods

		#region Constructors

		public EntryNoUnderlineEffect() 
			: base($"{ EffectGroupName.ResolutionGroupName}.{nameof(EntryNoUnderlineEffect)}")
		{
		}

		#endregion

		#endregion
	}
}
