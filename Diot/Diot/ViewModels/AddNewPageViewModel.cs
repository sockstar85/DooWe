using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Diot.Helpers;
using Diot.Interface;
using Diot.Models;
using Prism.Services;
using Xamarin.Forms;

namespace Diot.ViewModels
{
    public class AddNewPageViewModel : ViewModelBase
    {
        #region  Fields

        private ImageSource _coverImage;

        private MovieDbModel _currentResult;
        private int _currentResultIndex = -1;
        private string _movieTitle;
        private string _overview;
        private MovieDbResultsModel _searchResults;
        private IResourceManager _resourceManager;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the add new movie command.
        /// </summary>
        public ICommand AddNewMovieCommand => new Command(async () => { await addNewMovie(); });

        /// <summary>
        ///     Gets the search movie command.
        /// </summary>
        public ICommand SearchMovieCommand => new Command(async () => { await searchMovie(); });

        /// <summary>
        ///     Gets the next result command.
        /// </summary>
        public ICommand NextResultCommand => new Command(async () => { await loadNextResult(); });

        /// <summary>
        ///     Gets or sets the movie title.
        /// </summary>
        public string MovieTitle
        {
            get => _movieTitle;
            set => SetProperty(ref _movieTitle, value);
        }

        /// <summary>
        ///     Gets or sets the cover image.
        /// </summary>
        public ImageSource CoverImage
        {
            get => _coverImage;
            set => SetProperty(ref _coverImage, value);
        }

        /// <summary>
        ///     Gets or sets the current result.
        /// </summary>
        public MovieDbModel CurrentResult
        {
            get => _currentResult;
            set => SetProperty(ref _currentResult, value);
        }

        /// <summary>
        ///     Gets or sets the current result overview text.
        /// </summary>
        public string Overview
        {
            get => _overview;
            set => SetProperty(ref _overview, value);
        }

        #endregion

        #region Methods

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AddNewPageViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="pageDialogService">The page dialog service.</param>
        /// <param name="loadingPageService">The loading page service.</param>
        /// <param name="resourceManager">The resource manager.</param>
        /// <exception cref="ArgumentNullException">resourceManager</exception>
        public AddNewPageViewModel(IExtendedNavigation navigationService,
            IPageDialogService pageDialogService,
            ILoadingPageService loadingPageService,
            IResourceManager resourceManager) 
            : base(navigationService, pageDialogService, loadingPageService)
        {
            _resourceManager = resourceManager ?? throw new ArgumentNullException(nameof(resourceManager));
        }

        #endregion

        /// <summary>
        ///     Loads the next result.
        /// </summary>
        private async Task loadNextResult()
        {
            LoadingPageService.ShowLoadingPage(_resourceManager.GetString("Loading"));

            if (_searchResults?.Results == null || !_searchResults.Results.Any() ||
                _currentResultIndex + 1 >= _searchResults.Results.Count())
            {
                await DialogService.DisplayAlertAsync("End of results",
                    "There are no other search results found. Try again.", "Ok");

                await LoadingPageService.HideLoadingPageAsync();
                return;
            }

            CurrentResult = _searchResults.Results[++_currentResultIndex];

            await populateViewModel(CurrentResult);

            await LoadingPageService.HideLoadingPageAsync();
        }

        /// <summary>
        ///     Searches for the current movie title.
        /// </summary>
        private async Task searchMovie()
        {
            LoadingPageService.ShowLoadingPage(_resourceManager.GetString("Loading"));

            var results = await MoviesDbHelper.SearchMovie(MovieTitle);

            if (results?.Results != null && results.Results.Any())
            {
                _searchResults = results;
                CurrentResult = results.Results[0];
                _currentResultIndex = 0;

                await populateViewModel(CurrentResult);
            }
            else
            {
                await DialogService.DisplayAlertAsync("No search results",
                    $"No results found for \"{MovieTitle}\" found.", "Ok");
            }

            MovieTitle = string.Empty;

            await LoadingPageService.HideLoadingPageAsync();
        }

        /// <summary>
        ///     Populates the view model.
        /// </summary>
        private async Task populateViewModel(MovieDbModel currentResult)
        {
            if (currentResult?.Overview == null)
            {
                return;
            }

            Overview = currentResult.Overview;

            var imgSrc = await MoviesDbHelper.GetMovieCover(_currentResult);

            if (imgSrc == null || imgSrc.Length == 0)
            {
                Debug.WriteLine("No cover image found.");
                CoverImage = "library_icon.png";
                //TODO: Hide cover image
            }
            else
            {
                CoverImage = ImageSource.FromStream(() => new MemoryStream(imgSrc));
            };
        }

        /// <summary>
        ///     Adds the new movie.
        /// </summary>
        private async Task addNewMovie()
        {
            if (CurrentResult == null)
            {
                await DialogService.DisplayAlertAsync("Enter movie title", "Please search a movie title to add.", "Ok");
                return;
            }

            formatTitle(CurrentResult);

            DbService.SaveMovie(CurrentResult);

            await NavigationService.GoBackAsync();
        }

        /// <summary>
        ///     Formats the title with the "The" at the end of the title.
        /// </summary>
        /// <param name="currentResult">The current result.</param>
        private void formatTitle(MovieDbModel currentResult)
        {
            if (string.IsNullOrWhiteSpace(currentResult?.Title))
            {
                return;
            }

            var stringToMove = "The ";

            if (!currentResult.Title.StartsWith(stringToMove, StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            var title = currentResult.Title.Remove(0, stringToMove.Length);

            title += $", {stringToMove.Trim()}";

            currentResult.Title = title;
        }

        #endregion
    }
}