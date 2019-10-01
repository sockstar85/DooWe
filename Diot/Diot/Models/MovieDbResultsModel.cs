using System.Collections.Generic;

namespace Diot.Models
{
    public class MovieDbResultsModel
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the page.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        ///     Gets or sets the total results.
        /// </summary>
        public int TotalResults { get; set; }

        /// <summary>
        ///     Gets or sets the total pages.
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        ///     Gets or sets the results.
        /// </summary>
        public List<MovieDbModel> Results { get; set; } = new List<MovieDbModel>();

        #endregion
    }
}