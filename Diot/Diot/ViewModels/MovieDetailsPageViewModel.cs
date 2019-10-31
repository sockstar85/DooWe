using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Diot.Helpers;
using Diot.Interface;
using Diot.Interface.ViewModels;
using Diot.Models;
using Diot.Views.Pages;
using Prism.Navigation;
using Prism.Services;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using DryIoc;

namespace Diot.ViewModels
{
    public class MovieDetailsPageViewModel : ViewModelBase, IMovieDetailsPageViewModel
    {
        #region Fields

        MovieDbModel _selectedMovie;
        ImageSource _coverImage;
        private IDatabaseService _databaseService;
		private readonly IResourceManager _resourceManager;

		#endregion

		#region Properties

		/// <summary>
		///     Gets the delete movie command.
		/// </summary>
		public ICommand DeleteMovieCommand => new Command(async () =>
        {
            await deleteMovieAndNavigateBack();
        });

        /// <summary>
        ///     Gets or sets the selected movie.
        /// </summary>
        public MovieDbModel SelectedMovie
        {
            get => _selectedMovie;
            set => SetProperty(ref _selectedMovie, value);
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
		///		Gets the edit movie command.
		/// </summary>
		public ICommand EditMovieCommand => new Command(async () =>
		{
			await displayFormatSelectionPopup();
		});

		/// <summary>
		///		Gets or sets the refresh formats command.
		/// </summary>
		public ICommand RefreshFormatsCommand => new Command(refreshMovieFormats);
		
		#endregion

		#region Methods

		#region Constructors

		/// <summary>
		///		Initializes a new instance of the <see cref="MovieDetailsPageViewModel"/> class.
		/// </summary>
		/// <param name="navigationService">The navigation service.</param>
		/// <param name="dialogService">The dialog service.</param>
		/// <param name="loadingPageService">The loading page service.</param>
		/// <param name="databaseService">The database service.</param>
		/// <param name="resourceManger">The resource manger.</param>
		/// <exception cref="ArgumentNullException">
		/// databaseService
		/// or
		/// resourceManger
		/// </exception>
		public MovieDetailsPageViewModel(
            IExtendedNavigation navigationService, 
            IPageDialogService dialogService,
            ILoadingPageService loadingPageService,
            IDatabaseService databaseService,
			IResourceManager resourceManger) 
            : base(navigationService, dialogService, loadingPageService)
        {
            _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
			_resourceManager = resourceManger ?? throw new ArgumentNullException(nameof(resourceManger));
        }

        #endregion

        /// <summary>
        ///     Called when navigating to.
        /// </summary>
        /// <param name="parameters">The navigation parameters.</param>
        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            if (parameters == null)
            {
                throw new Exception("Navigation parameter cannot be null.");
            }

            parameters.TryGetValue(NavParamKeys.SelectedMovie, out MovieDbModel selectedMovie);

            var imgSrc = await MoviesDbHelper.GetMovieCover(selectedMovie);

            if (imgSrc == null)
            {
                CoverImage = selectedMovie?.CoverImage ?? "library_icon.png";
                //TODO: hide cover image
            }
            else
            {
                CoverImage = ImageSource.FromStream(() => new MemoryStream(imgSrc));
            };

            SelectedMovie = selectedMovie ?? throw new Exception("Selected movie cannot be null.");
        }

        /// <summary>
        ///     Deletes the movie and navigate back.
        /// </summary>
        private async Task deleteMovieAndNavigateBack()
        {
            if (SelectedMovie == null)
            {
                Console.WriteLine("Unable to remove title. Movie is null");
                return;
            }

            var confirmDelete = await DialogService.DisplayAlertAsync(
				_resourceManager.GetString("DeleteConfirmationTitle"),
                string.Format(_resourceManager.GetString("DeleteConfirmationMessage"), SelectedMovie.Title),
                _resourceManager.GetString("Yes"), 
				_resourceManager.GetString("No"));

            if (confirmDelete)
            {
                Console.WriteLine(_databaseService.DeleteMovie(SelectedMovie));
                await NavigationService.GoBackAsync();
            }
        }

		/// <summary>
		///		Displays the format selection popup.
		/// </summary>
		private async Task displayFormatSelectionPopup()
		{
			var movie = await App.AppContainer.Resolve<ISelectableMovieViewModel>().InitWithAsync(SelectedMovie);
			var formatSelectionPopup = new FormatSelectionPopupPage(movie)
			{
				AcceptCommand = RefreshFormatsCommand
			};

			await PopupNavigation.Instance.PushAsync(formatSelectionPopup);
		}

		/// <summary>
		///		Refreshes the movie formats.
		/// </summary>
		private void refreshMovieFormats(object obj)
		{
			ISelectableMovieViewModel selectableMovie = obj as ISelectableMovieViewModel;
			var movie = selectableMovie?.Movie;

			if (movie == null)
			{
				return;
			}

			//force a property changed
			SelectedMovie = new MovieDbModel();
			SelectedMovie = movie;

			//save changes to DB
			_databaseService.SaveMovie(movie);
		}

		#endregion
	}
}
