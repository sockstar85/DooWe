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
        ///     Initializes the view model with a <see cref="MovieDbModel"/>.
        /// </summary>
        /// <param name="movie">The movie.</param>
        /// <returns>Intialized view model.</returns>
        Task<ISelectableMovieViewModel> InitWithAsync(MovieDbModel movie);

        #endregion
    }
}
