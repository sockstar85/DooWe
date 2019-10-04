using System;
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
    public class MovieDetailsPageViewModel : ViewModelBase
    {
        #region Fields

        MovieDbModel _selectedMovie;
        ImageSource _coverImage;

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

        #endregion

        #region Methods

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MovieDetailsPageViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="dialogService">The dialog service.</param>
        public MovieDetailsPageViewModel(IExtendedNavigation navigationService, 
            IPageDialogService dialogService) 
            : base(navigationService, dialogService)
        {
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

            var confirmDelete = await DialogService.DisplayAlertAsync("Delete title?",
                $"Are you sure you want to remove {SelectedMovie.Title}?",
                "Yes", "No");

            if (confirmDelete)
            {
                Console.WriteLine(DbService.DeleteMovie(SelectedMovie));
                await NavigationService.GoBackAsync();
            }
        }

        #endregion
    }
}
