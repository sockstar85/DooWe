
using Diot.Helpers;
using Diot.Interface;
using Diot.Interface.ViewModels;
using Diot.Models;
using Prism.Services;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Diot.ViewModels
{
    public class SelectableMovieViewModel : ViewModelBase, ISelectableMovieViewModel
    {
        #region Fields

        private MovieDbModel _movie;
        private bool _isSelected;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the movie.
        /// </summary>
        public MovieDbModel Movie
        {
            get => _movie;
            set => SetProperty(ref _movie, value);
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        #endregion

        #region Methods

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SelectableMovieViewModel"/> class.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="loadingPageService">The loading page service.</param>
        public SelectableMovieViewModel() : base()
        {
        }

        /// <summary>
        ///     Initializes the view model from a <see cref="MovieDbModel"/>.
        /// </summary>
        public async Task<ISelectableMovieViewModel> InitWithAsync(IHttpClientService dataService, MovieDbModel movie)
        {
            var imgSrc = await MoviesDbHelper.GetMovieCover(dataService, movie);

			if (imgSrc != null)
			{
				movie.CoverImage = ImageSource.FromStream(() => new MemoryStream(imgSrc));
				movie.CoverImageByteArray = imgSrc;
			}

            Movie = movie;
            return this;
        }

        #endregion

        #endregion
    }
}
