using System;

namespace Diot.Helpers
{
    public static class MoviesDbApiUrls
    {
        #region  Fields

        private const string ApiKey = "5972f1e3fb034c0c6b6a1bc259488b93";

        #endregion

        #region Methods

        /// <summary>
        ///     Gets the search movie request URL.
        /// </summary>
        /// <param name="movieTitle">The movie title.</param>
        /// <returns></returns>
        public static string GetSearchMovieRequestUrl(string movieTitle)
        {
            return
                $"https://api.themoviedb.org/3/search/movie?api_key={ApiKey}&language=en-US&query={movieTitle}&page=1&include_adult=false";
        }

        /// <summary>
        ///     Gets the movie details request URL.
        /// </summary>
        /// <param name="movieId">The movie identifier.</param>
        public static string GetMovieDetailsRequestUrl(int movieId)
        {
            return $"https://api.themoviedb.org/3/movie/{movieId}?api_key={ApiKey}&language=en-US";
        }

        /// <summary>
        ///     Gets the movie poster request URL.
        /// </summary>
        /// <param name="imagePath">The image path.</param>
        /// <param name="size">The size.</param>
        public static string GetMoviePosterRequestUrl(string imagePath, int size)
        {
            if (size < 200)
            {
                size = 200;
            }
            else if (size > 800)
            {
                size = 800;
            }

			return $"https://image.tmdb.org/t/p/w{size}/{imagePath}";
        }

		/// <summary>
		///		Gets the movie cast and crew URL.
		/// </summary>
		/// <param name="id">The identifier.</param>
		internal static string GetMovieCastAndCrewUrl(int id)
		{
			return $"https://api.themoviedb.org/3/movie/{id}/credits?api_key={ApiKey}";
		}

		#endregion
	}
}