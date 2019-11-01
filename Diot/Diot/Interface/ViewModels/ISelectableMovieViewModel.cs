using Diot.Models;
using System.Threading.Tasks;

namespace Diot.Interface.ViewModels
{
    public interface ISelectableMovieViewModel : IViewModelBase
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the movie.
        /// </summary>
        MovieDbModel Movie { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        bool IsSelected { get; set; }

		#endregion

		#region Methods

		/// <summary>
		///		Initializes the view model.
		/// </summary>
		/// <param name="dataService">The data service.</param>
		/// <param name="movie">The movie.</param>
		Task<ISelectableMovieViewModel> InitWithAsync(IHttpClientService dataService, MovieDbModel movie);

        #endregion
    }
}
