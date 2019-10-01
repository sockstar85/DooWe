using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Diot.Helpers;
using Diot.Interface;
using Diot.Models;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace Diot.ViewModels
{
    public class LibraryPageViewModel : ViewModelBase
    {
        #region  Fields

        private List<MovieDbModel> _moviesList = new List<MovieDbModel>();

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the movies list.
        /// </summary>
        public List<MovieDbModel> MoviesList
        {
            get => _moviesList;
            set => SetProperty(ref _moviesList, value);
        }

        #endregion

        #region Methods

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="LibraryPageViewModel" /> class.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="dialogService">The dialog service.</param>
        public LibraryPageViewModel(IExtendedNavigation navigationService,
            IPageDialogService dialogService) : base(navigationService, dialogService)
        {
        }

        #endregion

        /// <summary>
        ///     Gets the cover images.
        /// </summary>
        private async Task getCoverImagesAsync()
        {
            var updatedList = new List<MovieDbModel>();

            foreach (var movie in MoviesList)
            {
                var imgSource = await MoviesDbHelper.GetMovieCover(movie.Poster_Path, 200);
                movie.CoverImage = imgSource == null ? "" : ImageSource.FromStream(() => new MemoryStream(imgSource));
                updatedList.Add(movie);
            }

            MoviesList = updatedList;
        }

        /// <summary>
        ///     Called when [navigating to].
        /// </summary>
        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            MoviesList = DbService.GetAllMovies();

            Task.Run(async () =>
            {
                await getCoverImagesAsync();
            });
        }

        #endregion
    }
}