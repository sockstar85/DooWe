using System;
using Diot.Interface;
using Diot.Interface.ViewModels;
using Diot.Services;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Xaml;

namespace Diot.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormatSelectionPopupPage
    {
        #region Fields

        private readonly ISelectableMovieViewModel _selectedMovie;
        private IResourceManager _resourceManager => ResourceManager.Instance;

        #endregion

        #region Methods

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="FormatSelectionPopupPage"/> class.
        /// </summary>
        /// <param name="selectedMovie">The selected movie.</param>
        public FormatSelectionPopupPage(ISelectableMovieViewModel selectedMovie)
        {
            InitializeComponent();

            if (string.IsNullOrWhiteSpace(selectedMovie?.Movie?.Title))
            {
                ClosePopup();
            }

            _selectedMovie = selectedMovie;

            initPage();
        }

        #endregion

        /// <summary>
        ///     Initializes the page.
        /// </summary>
        private void initPage()
        {
            Title.Text = string.Format(_resourceManager.GetString("FormatSelectionInstructions"), _selectedMovie.Movie.Title);
            Bluray.IsSelected = _selectedMovie.Movie.IsOnBluray;
            Dvd.IsSelected = _selectedMovie.Movie.IsOnDvd;
            Vudu.IsSelected = _selectedMovie.Movie.IsOnVudu;
            MoviesAnywhere.IsSelected = _selectedMovie.Movie.IsOnMoviesAnywhere;
            Amazon.IsSelected = _selectedMovie.Movie.IsOnAmazon;
            Other.IsSelected = _selectedMovie.Movie.IsOnOther;
        }

        #endregion

        #region Events

        /// <summary>
        ///     Closes the popup.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ClosePopup(object sender = null, EventArgs e = null)
        {
            PopupNavigation.Instance.PopAsync();
        }

        /// <summary>
        ///     Called when the Accept button is tapped on the popup.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AcceptTapped(object sender, EventArgs e)
        {

            _selectedMovie.Movie.IsOnBluray = Bluray.IsSelected;
            _selectedMovie.Movie.IsOnDvd = Dvd.IsSelected;
            _selectedMovie.Movie.IsOnVudu = Vudu.IsSelected;
            _selectedMovie.Movie.IsOnMoviesAnywhere = MoviesAnywhere.IsSelected;
            _selectedMovie.Movie.IsOnAmazon = Amazon.IsSelected;
            _selectedMovie.Movie.IsOnOther = Other.IsSelected;

            if (Other.IsSelected)
            {
                _selectedMovie.Movie.OtherComment = OtherComment.Text;
            }

            ClosePopup();
        }

        #endregion
    }
}