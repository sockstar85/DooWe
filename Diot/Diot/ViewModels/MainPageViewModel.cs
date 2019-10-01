﻿using System;
using System.Collections.Generic;
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
    public class MainPageViewModel : ViewModelBase
    {
        #region Fields

        private List<MovieDbModel> _moviesList = new List<MovieDbModel>();
        private MovieDbModel _selectedMovie;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the navigate to add new page command.
        /// </summary>
        public ICommand NavigateToAddNewPageCommand => new Command(async () => { await navigateToAddNewPage(); });

        /// <summary>
        ///     Gets or sets the movies list.
        /// </summary>
        public List<MovieDbModel> MoviesList
        {
            get => _moviesList;
            set => SetProperty(ref _moviesList, value);
        }

        public MovieDbModel SelectedMovie
        {
            get => _selectedMovie;
            set => SetProperty(ref _selectedMovie, value, async () => { await navigateToMovieDetailsPage(); });
        }

        #endregion

        #region Methods

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainPageViewModel" /> class.
        /// </summary>
        public MainPageViewModel(IExtendedNavigation navigationService, IPageDialogService dialogService)
            : base(navigationService, dialogService)
        {
        }

        #endregion

        /// <summary>
        ///     Navigates to add new page.
        /// </summary>
        private async Task navigateToAddNewPage()
        {
            var navigationResult = await NavigationService.NavigateAsync(PageNames.AddNewPage);

            if (!navigationResult.Success)
            {
                //TODO: handle failed navigation.
            }
        }

        /// <summary>
        ///     Navigates to movie details page.
        /// </summary>
        private async Task navigateToMovieDetailsPage()
        {
            if (SelectedMovie == null)
            {
                return;
            }

            var navigationParameters = new NavigationParameters
            {
                { NavParamKeys.SelectedMovie, SelectedMovie }
            };

            //reset the selected movie
            SelectedMovie = null;

            var navigationResults = await NavigationService.NavigateAsync(PageNames.MovieDetailsPage, navigationParameters);

            if (!navigationResults.Success)
            {
                //TODO: handle failed navigation.
            }
        }

        /// <summary>
        ///     Gets the cover images.
        /// </summary>
        private async Task getCoverImagesAsync()
        {
            var updatedList = new List<MovieDbModel>();

            foreach (var movie in MoviesList)
            {
                var imgSource = await MoviesDbHelper.GetMovieCover(movie.Poster_Path, 200);
                movie.CoverImage = imgSource == null ? "" : ImageSource.FromStream(() => new MemoryStream(imgSource));
                updatedList.Add(movie);
            }

            MoviesList = updatedList;
        }

        /// <summary>
        ///     Called when [navigating to].
        /// </summary>
        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            MoviesList = DbService.GetAllMovies();

            Task.Run(async () =>
            {
                await getCoverImagesAsync();
            });
        }

        #endregion
    }
}