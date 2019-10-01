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
            set => SetProperty(ref _currentResult, value, async () => { await populateViewModel(); });
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
        ///     Initializes a new instance of the <see cref="AddNewPageViewModel" /> class.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="pageDialogService">The page dialog service.</param>
        public AddNewPageViewModel(IExtendedNavigation navigationService,
            IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
        }

        #endregion

        /// <summary>
        ///     Loads the next result.
        /// </summary>
        private async Task loadNextResult()
        {
            if (_searchResults?.Results == null || !_searchResults.Results.Any() ||
                _currentResultIndex + 1 >= _searchResults.Results.Count())
            {
                await DialogService.DisplayAlertAsync("End of results",
                    "There are no other search results found. Try again.", "Ok");
                return;
            }

            CurrentResult = _searchResults.Results[++_currentResultIndex];
        }

        /// <summary>
        ///     Searches for the current movie title.
        /// </summary>
        private async Task searchMovie()
        {
            var results = await MoviesDbHelper.SearchMovie(MovieTitle);

            if (results?.Results != null && results.Results.Any())
            {
                _searchResults = results;
                CurrentResult = results.Results[0];
                _currentResultIndex = 0;
            }
            else
            {
                await DialogService.DisplayAlertAsync("No search results",
                    $"No results found for \"{MovieTitle}\" found.", "Ok");
            }

            MovieTitle = string.Empty;
        }

        /// <summary>
        ///     Populates the view model.
        /// </summary>
        private async Task populateViewModel()
        {
            Overview = CurrentResult.Overview;

            var imgSrc = await MoviesDbHelper.GetMovieCover(_currentResult.Poster_Path, 400);

            if (imgSrc == null)
            {
                Debug.WriteLine("No cover image found.");
                CoverImage = null;
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

            DbService.SaveMovie(CurrentResult);

            await NavigationService.GoBackAsync();
        }

        #endregion
    }
}