using System.Collections.Generic;

namespace Diot.Models
{
	public class MovieCreditsModel
	{
		#region Properties

		/// <summary>
		///		Gets or sets the cast.
		/// </summary>
		public IList<CastModel> Cast { get; set; } = new List<CastModel>();

		/// <summary>
		///		Gets or sets the crew.
		/// </summary>
		public IList<CrewModel> Crew { get; set; } = new List<CrewModel>();

		#endregion
	}
}
