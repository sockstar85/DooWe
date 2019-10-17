using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Diot.Helpers;
using Diot.Interface;
using Diot.Interface.ViewModels;
using Diot.Models;
using DryIoc;
using Prism.Services;
using Xamarin.Forms;

namespace Diot.ViewModels
{
    public class AddNewPageViewModel : ViewModelBase, IAddNewPageViewModel
    {
        #region  Fields

        private string _movieTitle;
        private List<ISelectableMovieViewModel> _searchResults = new List<ISelectableMovieViewModel>();
        private readonly IResourceManager _resourceManager;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the add selected movies command.
        /// </summary>
        public ICommand AddSelectedMoviesCommand => new Command(async () => { await addSelectedMovies(); });

        /// <summary>
        ///     Gets the search movie command.
        /// </summary>
        public ICommand SearchMovieCommand => new Command(async () => { await searchMovie(); });

        /// <summary>
        ///     Gets the select deselect movie command.
        /// </summary>
        public ICommand SelectDeselectMovieCommand => new Command<ISelectableMovieViewModel>((selection) => selectDeselectMovie(selection));

        /// <summary>
        ///     Gets or sets the movie title.
        /// </summary>
        public string MovieTitle
        {
            get => _movieTitle;
            set => SetProperty(ref _movieTitle, value);
        }

        /// <summary>
        ///     Gets or sets search results
        /// </summary>
        public List<ISelectableMovieViewModel> SearchResults
        {
            get => _searchResults;
            set => SetProperty(ref _searchResults, value);
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
        ///     Selects/deselect the movie.
        /// </summary>
        /// <param name="selectedMovie">The selected movie.</param>
        private void selectDeselectMovie(ISelectableMovieViewModel selectedMovie)
        {
            if (selectedMovie == null)
            {
                return;
            }

            selectedMovie.IsSelected = !selectedMovie.IsSelected;
        }

        /// <summary>
        ///     Searches for the current movie title.
        /// </summary>
        private async Task searchMovie()
        {
            LoadingPageService.ShowLoadingPage(string.Format(_resourceManager.GetString("SearchingMovie"), MovieTitle));

            var results = await MoviesDbHelper.SearchMovie(MovieTitle);

            if (results?.Results != null && results.Results.Any())
            {
                SearchResults = await convertToSelectableMovieViewModelsAsync(results.Results); //TODO initialize with interface
            }
            else
            {
                await DialogService.DisplayAlertAsync(
                    _resourceManager.GetString("NoSearchResults"),
                    string.Format(_resourceManager.GetString("NoSearchResultsMessage"), MovieTitle),
                    _resourceManager.GetString("Ok"));
            }

            MovieTitle = string.Empty;

            await LoadingPageService.HideLoadingPageAsync();
        }

        private async Task<List<ISelectableMovieViewModel>> convertToSelectableMovieViewModelsAsync(List<MovieDbModel> results)
        {
            var retVal = new List<ISelectableMovieViewModel>();

            LoadingPageService.UpdateText(string.Format(_resourceManager.GetString("PossibleMatchesText"), results.Count));

            foreach(var item in results)
            {
                retVal.Add(await App.AppContainer.Resolve<ISelectableMovieViewModel>().InitWithAsync(item));
            }

            return retVal;
        }

        /// <summary>
        ///     Adds the selected movies to the DB and navigates home.
        /// </summary>
        private async Task addSelectedMovies()
        {
            //TODO: finish this
            await Task.Run(() => { });
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