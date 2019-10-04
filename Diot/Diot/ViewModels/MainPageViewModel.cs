using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Diot.Helpers;
using Diot.Interface;
using Diot.Models;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace Diot.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        #region Fields

        private List<MovieDbModel> _moviesList = new List<MovieDbModel>();

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the navigate to add new page command.
        /// </summary>
        public ICommand NavigateToAddNewPageCommand => new Command(async () => { await navigateToAddNewPage(); });

        /// <summary>
        ///     Gets the  navigate to movie details command.
        /// </summary>
        public ICommand NavigateToMovieDetailsCommand => new Command<MovieDbModel>(async (selection) => { await navigateToMovieDetailsPage(selection); });

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
        ///     Initializes a new instance of the <see cref="MainPageViewModel" /> class.
        /// </summary>
        public MainPageViewModel(IExtendedNavigation navigationService, IPageDialogService dialogService)
            : base(navigationService, dialogService)
        {
        }

        #endregion

        /// <summary>
        ///     Navigates to add new page.
        /// </summary>
        private async Task navigateToAddNewPage()
        {
            //TODO: Check for data connectivity first

            var navigationResult = await NavigationService.NavigateAsync(PageNames.AddNewPage);

            if (!navigationResult.Success)
            {
                //TODO: handle failed navigation.
            }
        }

        /// <summary>
        ///     Navigates to movie details page.
        /// </summary>
        private async Task navigateToMovieDetailsPage(MovieDbModel selectedMovie)
        {
            if (selectedMovie == null)
            {
                return;
            }

            var navigationParameters = new NavigationParameters
            {
                { NavParamKeys.SelectedMovie, selectedMovie }
            };

            //reset the selected movie
            selectedMovie = null;

            var navigationResults = await NavigationService.NavigateAsync(PageNames.MovieDetailsPage, navigationParameters);

            if (!navigationResults.Success)
            {
                //TODO: handle failed navigation.
            }
        }

        /// <summary>
        ///     Gets the cover images.
        /// </summary>
        private async Task getCoverImagesAsync()
        {
            var updatedList = new List<MovieDbModel>();

            foreach (var movie in MoviesList)
            {
                if (movie == null)
                {
                    continue;
                }

                var imgSource = await MoviesDbHelper.GetMovieCover(movie);

                movie.CoverImage = imgSource == null 
                    ? "library_icon.png"
                    : ImageSource.FromStream(() => new MemoryStream(imgSource));

                //update the movie in the DB with the new byte array
                if (imgSource != null && movie.CoverImageByteArray != imgSource)
                {
                    movie.CoverImageByteArray = imgSource;
                    DbService.SaveMovie(movie);
                }

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