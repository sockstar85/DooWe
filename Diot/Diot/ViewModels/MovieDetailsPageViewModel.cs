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
using Diot.Interface.Manager;
using System.Collections.Generic;
using System.Linq;

namespace Diot.ViewModels
{
    public class MovieDetailsPageViewModel : NavigatableViewModel, IMovieDetailsPageViewModel
    {
        #region Fields

        private MovieDbModel _selectedMovie;
        private ImageSource _coverImage;
        private IDatabaseService _databaseService;
		private ImageSource _backdropImage;
		private bool _foundBackdropImage;
		private string _starringText;
		private string _directorText;
		private readonly IResourceManager _resourceManager;
		private readonly IHttpClientService _dataService;

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

		/// <summary>
		///		Gets the navigate back command.
		/// </summary>
		public ICommand NavigateBackCommand => new Command(async () =>
		{
			await NavigationService.GoBackAsync();
		});

		/// <summary>
		///		Gets or sets the backdrop image.
		/// </summary>
		public ImageSource BackdropImage 
		{ 
			get => _backdropImage; 
			set => SetProperty(ref _backdropImage, value); 
		}

		/// <summary>
		///		Gets or sets a value indicating whether found backdrop image.
		/// </summary>
		public bool FoundBackdropImage 
		{
			get => _foundBackdropImage; 
			set => SetProperty(ref _foundBackdropImage, value); 
		}

		/// <summary>
		///		Gets or sets the starring text.
		/// </summary>
		public string StarringText
		{
			get => _starringText;
			set => SetProperty(ref _starringText, value);
		}

		/// <summary>
		/// Gets or sets the director text.
		/// </summary>
		public string DirectorText
		{
			get => _directorText;
			set => SetProperty(ref _directorText, value);
		}

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
			IResourceManager resourceManger,
			IHttpClientService dataService,
			IConnectivityManager connectivityManager) 
            : base(navigationService, dialogService, loadingPageService,
				  connectivityManager)
        {
            _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
			_resourceManager = resourceManger ?? throw new ArgumentNullException(nameof(resourceManger));
			_dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        }

		#endregion

		/// <summary>
		///		Initializes the view model asynchronously.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		/// <exception cref="Exception">
		public override async Task InitializeAsync(INavigationParameters parameters)
		{
			if (parameters == null)
			{
				throw new Exception("Navigation parameter cannot be null.");
			}

			parameters.TryGetValue(NavParamKeys.SelectedMovie, out MovieDbModel selectedMovie);

			SelectedMovie = selectedMovie ?? throw new ArgumentNullException("Selected movie cannot be null.");

			if (HasNetworkConnection)
			{
				try
				{
					SelectedMovie = await MoviesDbHelper.GetMovieDetailsAsync(_dataService, selectedMovie.Id);
					var imgSource = await MoviesDbHelper.GetMovieBackgroundAsync(_dataService, selectedMovie);
					BackdropImage = ImageSource.FromStream(() => new MemoryStream(imgSource));
					FoundBackdropImage = true;
					StarringText = getStarringText(SelectedMovie.Credits.Cast);
					DirectorText = getDirectorText(SelectedMovie.Credits.Crew);

				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
					CoverImage = await getCoverImageFromCacheAsync(selectedMovie);
				}
			}
			else
			{
				CoverImage = await getCoverImageFromCacheAsync(selectedMovie);
			}
		}

		/// <summary>
		///		Gets the director text.
		/// </summary>
		/// <param name="crew">The crew.</param>
		private string getDirectorText(IList<CrewModel> crew)
		{
			var director = crew?.FirstOrDefault(x => x.Job.ToLower() == "director");			

			if (director == null)
			{
				return null;
			}

			return $"{_resourceManager.GetString("Director")}: {director.Name}";
		}

		/// <summary>
		///		Gets the starring text.
		/// </summary>
		/// <param name="cast">The cast.</param>
		private string getStarringText(IList<CastModel> cast)
		{
			if (cast == null || cast.Count == 0)
			{
				return null;
			}

			var retVal = $"{_resourceManager.GetString("Starring")}: ";

			//just display the first 5 cast members
			for (var i = 0; i < 5 && i < cast.Count; i++)
			{
				var castMember = cast[i];

				if (i > 0)
				{
					retVal += ", ";
				}

				retVal += castMember.Name;
			}

			return retVal;
		}

		private async Task<ImageSource> getCoverImageFromCacheAsync(MovieDbModel selectedMovie)
		{
			var imgSrc = await MoviesDbHelper.GetMovieCoverAsync(_dataService, selectedMovie);

			if (imgSrc == null)
			{
				return selectedMovie?.CoverImage ?? "library_icon.png";
				//TODO: hide cover image
			}
			else
			{
				return ImageSource.FromStream(() => new MemoryStream(imgSrc));
			};
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
			var movie = await App.AppContainer.Resolve<ISelectableMovieViewModel>().InitWithAsync(_dataService, SelectedMovie);
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
