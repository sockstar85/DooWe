using System;
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
    public class MovieDetailsPageViewModel : ViewModelBase
    {
        #region Fields

        MovieDbModel _selectedMovie;
        ImageSource _coverImage;

        #endregion

        #region Properties

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

            var imgSrc = await MoviesDbHelper.GetMovieCover(selectedMovie.Poster_Path, 300);

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

        #endregion
    }
}
