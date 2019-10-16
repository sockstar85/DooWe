using System;
using System.Diagnostics;
using System.Linq;
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
                retVal.Results = retVal.Results.OrderByDescending(x => x.Popularity).ThenByDescending(x => x.Vote_Count).ToList();
            });

            return retVal;
        }

        /// <summary>
        ///     Gets the movie cover.
        /// </summary>
        /// <param name="posterPath">The poster path.</param>
        /// <param name="size">The size.</param>
        public static async Task<byte[]> GetMovieCover(MovieDbModel movie)
        {
            if (string.IsNullOrWhiteSpace(movie?.Poster_Path))
            {
                Debug.WriteLine("Empty poster path");
                return null;
            }

            if (movie.CoverImageByteArray != null && movie.CoverImageByteArray.Length > 0)
            {
                return movie.CoverImageByteArray;
            }

            try
            {
                return await new HttpClientService().GetImageByteArrayAsync(
                    MoviesDbApiUrls.GetMoviePosterRequestUrl(movie.Poster_Path, 300));
            }
            catch (Exception)
            {
                 return null;
            }
        }

        #endregion
    }
}