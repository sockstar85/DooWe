using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Diot.Helpers;
using Diot.Interface;
using Diot.Interface.ViewModels;
using Diot.Models;
using Diot.Views.Pages;
using DryIoc;
using Plugin.Connectivity;
using Prism.Services;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Diot.ViewModels
{
    public class AddNewPageViewModel : NavigatableViewModel, IAddNewPageViewModel
    {
        #region  Fields

        private string _movieTitle;
        private ObservableCollection<ISelectableMovieViewModel> _searchResults = new ObservableCollection<ISelectableMovieViewModel>();
        private readonly IResourceManager _resourceManager;
        private readonly IDatabaseService _databaseService;
        private readonly IPageDialogService _dialogService;
		private readonly IHttpClientService _dataService;

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
        public ICommand SelectDeselectMovieCommand => new Command<ISelectableMovieViewModel>(async (selection) => await selectDeselectMovieAsync(selection));

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
        public ObservableCollection<ISelectableMovieViewModel> SearchResults
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
        /// <param name="databaseService">The database service.</param>
        public AddNewPageViewModel(IExtendedNavigation navigationService,
            IPageDialogService pageDialogService,
            ILoadingPageService loadingPageService,
            IResourceManager resourceManager,
            IDatabaseService databaseService,
			IHttpClientService dataService) 
            : base(navigationService, pageDialogService, loadingPageService)
        {
            _resourceManager = resourceManager ?? throw new ArgumentNullException(nameof(resourceManager));
            _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
            _dialogService = pageDialogService ?? throw new ArgumentNullException(nameof(pageDialogService));
			_dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        }

        #endregion

        /// <summary>
        ///     Selects/deselect the movie.
        /// </summary>
        /// <param name="selectedMovie">The selected movie.</param>
        private async Task selectDeselectMovieAsync(ISelectableMovieViewModel selectedMovie)
        {
            if (selectedMovie == null)
            {
                return;
            }

            selectedMovie.IsSelected = !selectedMovie.IsSelected;

            if (selectedMovie.IsSelected)
            {
				var popup = new FormatSelectionPopupPage(selectedMovie)
				{
					CancelCommand = new Command(() => { cancelSelection(selectedMovie); })
				};

				await PopupNavigation.Instance.PushAsync(popup);
            }
        }


		/// <summary>
		///		Cancels the selection.
		/// </summary>
		/// <param name="movie">The movie.</param>
		private void cancelSelection(ISelectableMovieViewModel movie)
		{
			if (movie == null)
			{
				return;
			}

			movie.IsSelected = false;
		}

		/// <summary>
		///     Searches for the current movie title.
		/// </summary>
		private async Task searchMovie()
        {			
            if (!CrossConnectivity.Current.IsConnected)
            {
                await _dialogService.DisplayAlertAsync(
                    _resourceManager.GetString("NoNetworkConnection"),
                    _resourceManager.GetString("NoNetworkConnectionMessage"),
                    _resourceManager.GetString("Ok"));

                return;
            }

            LoadingPageService.ShowLoadingPage(string.Format(_resourceManager.GetString("SearchingMovie"), MovieTitle?.Trim()));

            MovieDbResultsModel results = null;

            try
            {
                results = await MoviesDbHelper.SearchMovie(_dataService, MovieTitle);
            }
            catch (Exception)
            {
                await _dialogService.DisplayAlertAsync(
                    _resourceManager.GetString("GenericErrorTitle"),
                    _resourceManager.GetString("GenericErrorMessage"),
                    _resourceManager.GetString("Ok"));
                return;
            }

            if (results?.Results != null && results.Results.Any())
            {
				SearchResults = await convertToSelectableMovieViewModelsAsync(results.Results);
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

        private async Task<ObservableCollection<ISelectableMovieViewModel>> convertToSelectableMovieViewModelsAsync(List<MovieDbModel> results)
        {
            var retVal = new ObservableCollection<ISelectableMovieViewModel>();

            LoadingPageService.UpdateText(_resourceManager.GetString("PossibleMatchesText"));

            foreach(var item in results)
            {
				//If there isn't a poster path in the api DB it's likely not one we want.
				if (string.IsNullOrWhiteSpace(item.Poster_Path))
				{
					continue;
				}

                var movie = await App.AppContainer.Resolve<ISelectableMovieViewModel>().InitWithAsync(_dataService, item);
                formatTitle(movie);

                retVal.Add(movie);
            }

            return retVal;
        }

        /// <summary>
        ///     Adds the selected movies to the DB and navigates home.
        /// </summary>
        private async Task addSelectedMovies()
        {
            LoadingPageService.ShowLoadingPage(_resourceManager.GetString("AddingMovies"));

            var selectedMovies = SearchResults?.Where(x => x.IsSelected).ToList() ?? new List<ISelectableMovieViewModel>();

            foreach (var movie in selectedMovies)
            {
                formatTitle(movie);

                await Task.Run(() =>
                {
                    return _databaseService.SaveMovie(movie);
                });
            }

            await NavigationService.GoBackAsync();

            await LoadingPageService.HideLoadingPageAsync();
        }

        /// <summary>
        ///     Formats the title with the "The" at the end of the title.
        /// </summary>
        /// <param name="selection">The selected movie.</param>
        private void formatTitle(ISelectableMovieViewModel selection)
        {
            if (string.IsNullOrWhiteSpace(selection?.Movie?.Title))
            {
                return;
            }

            var stringToMove = "The ";

            if (!selection.Movie.Title.StartsWith(stringToMove, StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            var title = selection.Movie.Title.Remove(0, stringToMove.Length);

            title += $", {stringToMove.Trim()}";

            selection.Movie.Title = title;
        }

        #endregion
    }
}