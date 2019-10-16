using System.Collections.Generic;
using Diot.Models;

namespace Diot.Interface
{
    public interface IDatabaseService
    {
        #region Methods

        /// <summary>
        ///     Gets all movies.
        /// </summary>
        List<MovieDbModel> GetAllMovies();

        /// <summary>
        ///     Saves the movie.
        /// </summary>
        int SaveMovie(MovieDbModel movie);

        /// <summary>
        ///     Deletes the movie.
        /// </summary>
        int DeleteMovie(MovieDbModel movie);

        /// <summary>
        ///     Deletes all movies.
        /// </summary>
        List<MovieDbModel> DeleteAllMovies();

        #endregion
    }
}
