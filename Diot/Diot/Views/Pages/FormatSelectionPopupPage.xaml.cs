﻿using System;
using System.Windows.Input;
using Diot.Interface;
using Diot.Interface.ViewModels;
using Diot.Models;
using Diot.Services;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Diot.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormatSelectionPopupPage
    {
        #region Fields

		private IResourceManager _resourceManager => ResourceManager.Instance;
        private readonly ISelectableMovieViewModel _selectedMovie;

		#endregion

		#region Properties

		/// <summary>
		///		Gets or sets the refresh command.
		/// </summary>
		public ICommand RefreshCommand
		{
			get => (ICommand)GetValue(RefreshCommandProperty);
			set => SetValue(RefreshCommandProperty, value);
		}
		
		/// <summary>
		///		Gets or sets the accept command.
		/// </summary>
		public ICommand AcceptCommand
		{
			get => (ICommand)GetValue(AcceptCommandProperty);
			set => SetValue(AcceptCommandProperty, value);
		}

		/// <summary>
		///		Gets or sets the cancel command.
		/// </summary>
		public ICommand CancelCommand
		{
			get => (ICommand)GetValue(CancelCommandProperty);
			set => SetValue(CancelCommandProperty, value);
		}
		#endregion

		#region Bindable Properties

		/// <summary>
		///		The bindable property for <see cref="AcceptCommand"/>.
		/// </summary>
		public static readonly BindableProperty AcceptCommandProperty =
			BindableProperty.Create(
				nameof(AcceptCommand),
				typeof(ICommand),
				typeof(FormatSelectionPopupPage));

		/// <summary>
		///		The bindable property for <see cref="CancelCommand"/>.
		/// </summary>
		public static readonly BindableProperty CancelCommandProperty =
			BindableProperty.Create(
				nameof(CancelCommand),
				typeof(ICommand),
				typeof(FormatSelectionPopupPage));

		/// <summary>
		///		The bindable property for <see cref="RefreshCommand"/>.
		/// </summary>
		public static readonly BindableProperty RefreshCommandProperty =
			BindableProperty.Create(
				nameof(RefreshCommand),
				typeof(ICommand),
				typeof(FormatSelectionPopupPage));

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
                closePopup();
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
            Instructions.Text = string.Format(_resourceManager.GetString("FormatSelectionInstructions"), _selectedMovie.Movie.Title);
            Bluray.IsSelected = _selectedMovie.Movie.IsOnBluray;
            Dvd.IsSelected = _selectedMovie.Movie.IsOnDvd;
            Vudu.IsSelected = _selectedMovie.Movie.IsOnVudu;
            MoviesAnywhere.IsSelected = _selectedMovie.Movie.IsOnMoviesAnywhere;
            Amazon.IsSelected = _selectedMovie.Movie.IsOnAmazon;
            Plex.IsSelected = _selectedMovie.Movie.IsOnPlex;
            Other.IsSelected = _selectedMovie.Movie.IsOnOther;
            OtherComment.Text = _selectedMovie.Movie.IsOnOther ? _selectedMovie.Movie.OtherComment : string.Empty;
        }

        #endregion

        #region Events

        /// <summary>
        ///     Closes the popup.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void closePopup()
        {
            PopupNavigation.Instance.PopAsync();
        }

        /// <summary>
        ///     Called when the Accept button is tapped on the popup.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void acceptTapped(object sender, EventArgs e)
        {

            _selectedMovie.Movie.IsOnBluray = Bluray.IsSelected;
            _selectedMovie.Movie.IsOnDvd = Dvd.IsSelected;
            _selectedMovie.Movie.IsOnVudu = Vudu.IsSelected;
            _selectedMovie.Movie.IsOnMoviesAnywhere = MoviesAnywhere.IsSelected;
            _selectedMovie.Movie.IsOnPlex = Plex.IsSelected;
            _selectedMovie.Movie.IsOnAmazon = Amazon.IsSelected;
            _selectedMovie.Movie.IsOnOther = Other.IsSelected;

            if (Other.IsSelected)
            {
                _selectedMovie.Movie.OtherComment = OtherComment.Text;
            }

			AcceptCommand?.Execute(_selectedMovie);
            closePopup();
        }


		/// <summary>
		///		Called when the Cancel button is tapped on the popup.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void cancelTapped(object sender, EventArgs e)
		{
			CancelCommand?.Execute(_selectedMovie);
			closePopup();
		}

        #endregion
    }
}