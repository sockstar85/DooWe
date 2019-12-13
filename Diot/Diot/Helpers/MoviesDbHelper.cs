using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Diot.Interface;
using Diot.Models;
using Newtonsoft.Json;
using DryIoc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Diot.Helpers
{
    public static class MoviesDbHelper
    {
		#region Methods

		/// <summary>
		///     Searches for a movie.
		/// </summary>
		/// <param name="movieTitle">The movie title.</param>
		public static async Task<MovieDbResultsModel> SearchMovieAsync(IHttpClientService dataService, string movieTitle)
		{
			if (dataService == null)
			{
				dataService = App.AppContainer.Resolve<IHttpClientService>();
			}

            if (string.IsNullOrWhiteSpace(movieTitle))
            {
                Debug.WriteLine($"Unknown movie title: \"{movieTitle}\"");
                return null;
            }

            var result = await dataService.GetStringAsync(
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
        public static async Task<byte[]> GetMovieCoverAsync(IHttpClientService dataService, MovieDbModel movie)
        {
			if (dataService == null)
			{
				dataService = App.AppContainer.Resolve<IHttpClientService>();
			}

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
                return await dataService.GetImageByteArrayAsync(
                    MoviesDbApiUrls.GetMoviePosterRequestUrl(movie.Poster_Path, 300));
            }
            catch (Exception)
            {
                 return null;
            }
        }

		/// <summary>
		///		Gets the movie background.
		/// </summary>
		/// <param name="dataService">The data service.</param>
		/// <param name="selectedMovie">The selected movie.</param>
		public static async Task<byte[]> GetMovieBackgroundAsync(IHttpClientService dataService, MovieDbModel selectedMovie)
		{
			if (dataService == null)
			{
				dataService = App.AppContainer.Resolve<IHttpClientService>();
			}

			var movie = selectedMovie;

			try
			{
				if (string.IsNullOrWhiteSpace(selectedMovie.Backdrop_Path))
				{
					var movieJson = await dataService.GetStringAsync(
						MoviesDbApiUrls.GetMovieDetailsRequestUrl(selectedMovie.Id));

					movie = JsonConvert.DeserializeObject<MovieDbModel>(movieJson);
				}

				return await dataService.GetImageByteArrayAsync(
						MoviesDbApiUrls.GetMoviePosterRequestUrl(movie.Backdrop_Path, 500));
			}
			catch (Exception)
			{
				return null;
			}
		}

		/// <summary>
		///		Gets the movie details.
		/// </summary>
		/// <param name="dataService">The data service.</param>
		/// <param name="id">The identifier.</param>
		public static async Task<MovieDbModel> GetMovieDetailsAsync(IHttpClientService dataService, int id)
		{
			MovieDbModel details = null;

			try
			{
				var json = await dataService.GetStringAsync(MoviesDbApiUrls.GetMovieDetailsRequestUrl(id));

				details = JsonConvert.DeserializeObject<MovieDbModel>(json);

				var castAndCrewJson = await dataService.GetStringAsync(MoviesDbApiUrls.GetMovieCastAndCrewUrl(id));
				var castAndCrew = JObject.Parse(castAndCrewJson);

				var cast = castAndCrew["cast"].ToObject<List<CastModel>>();
				var crew = castAndCrew["crew"].ToObject<List<CrewModel>>();

				details.Credits = new MovieCreditsModel
				{
					Cast = cast,
					Crew = crew
				};
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

			return details;
		}

		#endregion
	}
}