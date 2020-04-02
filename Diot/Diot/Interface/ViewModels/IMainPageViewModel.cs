using System.Collections.Generic;
using System.Windows.Input;
using Diot.Models;

namespace Diot.Interface.ViewModels
{
    public interface IMainPageViewModel : IViewModelBase
    {
        #region Properties

        /// <summary>
        ///     Gets the navigate to add new page command.
        /// </summary>
        ICommand NavigateToAddNewPageCommand { get; }

        /// <summary>
        ///     Gets the navigate to movie details command.
        /// </summary>
        ICommand NavigateToMovieDetailsCommand { get; }

		/// <summary>
		///		Gets the search command.
		/// </summary>
		ICommand SearchCommand { get; }

		/// <summary>
		///		Gets or sets the title search criteria.
		/// </summary>
		string TitleSearch { get; set; }

        /// <summary>
        ///     Gets or sets the movies list.
        /// </summary>
        IList<MovieDbModel> MoviesList { get; set; }

		/// <summary>
		///     Gets or sets the sorted movies list.
		/// </summary>
		IList<MovieDbModel> SortedMoviesList { get; set; }

		/// <summary>
		///     Gets or sets a value indicating whether this instance has no movies.
		/// </summary>
		bool HasNoMovies { get; set; }

        #endregion
    }
}
