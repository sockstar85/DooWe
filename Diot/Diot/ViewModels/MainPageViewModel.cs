using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Diot.Helpers;
using Diot.Interface;
using Diot.Interface.ViewModels;
using Diot.Models;
using Plugin.Connectivity;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace Diot.ViewModels
{
    public class MainPageViewModel : ViewModelBase, IMainPageViewModel
    {
        #region Fields

        private List<MovieDbModel> _moviesList = new List<MovieDbModel>();
        private bool _hasNoMovies;
        private readonly IDatabaseService _databaseService;
        private readonly IPageDialogService _dialogService;
        private readonly IResourceManager _resourceManager;

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
            set => SetProperty(ref _moviesList, value, updateNoMoviesInstructionVisibility);
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance has no movies.
        /// </summary>
        public bool HasNoMovies
        {
            get => _hasNoMovies;
            set => SetProperty(ref _hasNoMovies, value);
        }

        #endregion

        #region Methods

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainPageViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="loadingPageService">The loading page service.</param>
        public MainPageViewModel(
            IExtendedNavigation navigationService, 
            IPageDialogService dialogService, 
            ILoadingPageService loadingPageService,
            IDatabaseService databaseService,
            IResourceManager resourceManager)
            : base(navigationService, dialogService, loadingPageService)
        {
            _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            _resourceManager = resourceManager ?? throw new ArgumentNullException(nameof(resourceManager));
        }

        #endregion

        /// <summary>
        ///     Navigates to add new page.
        /// </summary>
        private async Task navigateToAddNewPage()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                await _dialogService.DisplayAlertAsync(
                    _resourceManager.GetString("NoNetworkConnection"), 
                    _resourceManager.GetString("NoNetworkConnectionMessage"),
                    _resourceManager.GetString("Ok"));

                return;
            }


            var navigationResult = await NavigationService.NavigateAsync(PageNames.AddNewPage);

            if (!navigationResult.Success)
            {
                await handleFailedNavigationAsync(navigationResult.Exception);
            }
        }

        /// <summary>
        ///     Handles the failed navigation.
        /// </summary>
        /// <param name="exception">The exception.</param>
        private async Task handleFailedNavigationAsync(Exception exception)
        {
            Console.WriteLine(exception.Message);

            await _dialogService.DisplayAlertAsync(
                _resourceManager.GetString("NavigationError"),
                _resourceManager.GetString("NavigationErrorMessage"),
                _resourceManager.GetString("Ok"));
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

            var navigationResult = await NavigationService.NavigateAsync(PageNames.MovieDetailsPage, navigationParameters);

            if (!navigationResult.Success)
            {
                await handleFailedNavigationAsync(navigationResult.Exception);
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
                    _databaseService.SaveMovie(movie);
                }

                updatedList.Add(movie);
            }

            MoviesList = updatedList;
        }

        /// <summary>
        ///     Called when navigating to.
        /// </summary>
        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            try
            {
                MoviesList = _databaseService.GetAllMovies();

                await getCoverImagesAsync();
            }
            catch (Exception)
            {
                await _dialogService.DisplayAlertAsync(
                    _resourceManager.GetString("GenericErrorTitle"),
                    _resourceManager.GetString("GenericErrorMessage"),
                    _resourceManager.GetString("Ok"));
            }
        }

        /// <summary>
        ///     Updates the no movies instruction visibility.
        /// </summary>
        private void updateNoMoviesInstructionVisibility()
        {
            HasNoMovies = MoviesList == null || !MoviesList.Any();
        }

        #endregion
    }
}