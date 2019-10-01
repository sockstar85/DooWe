using Diot.Interface;
using Prism.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Diot.Helpers;
using Diot.Services;
using Xamarin.Forms;

namespace Diot.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        #region Properties

        public ICommand DeleteAllMoviesCommand => new Command(deleteAllMoviesAsync);

        #endregion

        #region Methods

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SettingsPageViewModel" /> class.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="dialogService">The dialog service.</param>
        public SettingsPageViewModel(IExtendedNavigation navigationService, IPageDialogService dialogService) 
            : base(navigationService, dialogService)
        {
        }

        #endregion


        /// <summary>
        ///     Deletes all movies.
        /// </summary>
        private void deleteAllMoviesAsync()
        {
            DbService.DeleteAllMovies();
        }

        #endregion
    }
}