using System.Collections.Generic;
using Diot.Interface.ViewModels;
using Diot.Models;

namespace Diot.Interface
{
    public interface IDatabaseService
    {
        #region Methods

        /// <summary>
        ///     Gets all movies.
        /// </summary>
        IList<MovieDbModel> GetAllMovies();

        /// <summary>
        ///     Saves the movie.
        /// </summary>
        /// <param name="movie">The movie.</param>
        int SaveMovie(MovieDbModel movie);

        /// <summary>
        ///     Saves the movie.
        /// </summary>
        /// <param name="movie">The movie.</param>
        int SaveMovie(ISelectableMovieViewModel movie);

        /// <summary>
        ///     Deletes the movie.
        /// </summary>
        int DeleteMovie(MovieDbModel movie);

        /// <summary>
        ///     Deletes all movies.
        /// </summary>
        IList<MovieDbModel> DeleteAllMovies();

        #endregion
    }
}
