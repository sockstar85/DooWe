using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Diot.Models;
using Diot.Services;
using Newtonsoft.Json;

namespace Diot.Helpers
{
    public static class MoviesDbHelper
    {
        #region Methods

        /// <summary>
        ///     Searches for a movie.
        /// </summary>
        /// <param name="movieTitle">The movie title.</param>
        public static async Task<MovieDbResultsModel> SearchMovie(string movieTitle)
        {
            if (string.IsNullOrWhiteSpace(movieTitle))
            {
                Debug.WriteLine($"Unknown movie title: \"{movieTitle}\"");
                return null;
            }

            var result = await new HttpClientService().GetStringAsync(
                MoviesDbApiUrls.GetSearchMovieRequestUrl(movieTitle));

            MovieDbResultsModel retVal = null;

            await Task.Run(() =>
            {
                retVal = JsonConvert.DeserializeObject<MovieDbResultsModel>(result);
            });

            return retVal;
        }

        /// <summary>
        ///     Gets the movie cover.
        /// </summary>
        /// <param name="posterPath">The poster path.</param>
        /// <param name="size">The size.</param>
        public static async Task<byte[]> GetMovieCover(string posterPath, int size)
        {
            if (string.IsNullOrWhiteSpace(posterPath))
            {
                Debug.WriteLine("Empty poster path");
                return null;
            }

            try
            {
                return await new HttpClientService().GetImageByteArrayAsync(
                    MoviesDbApiUrls.GetMoviePosterRequestUrl(posterPath, size));
            }
            catch (Exception)
            {
                 return null;
            }
        }

        #endregion
    }
}