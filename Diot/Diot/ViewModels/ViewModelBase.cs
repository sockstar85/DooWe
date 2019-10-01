using System;
using Diot.Interface;
using Diot.Services;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace Diot.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        #region  Fields

        private static DatabaseService databaseService;
        private string _title;

        #endregion

        #region Properties

        public IExtendedNavigation NavigationService { get; }
        public IPageDialogService DialogService { get; }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        /// <summary>
        ///     Gets the database service.
        /// </summary>
        public static DatabaseService DbService => databaseService ?? (databaseService = new DatabaseService());

        #endregion

        #region Methods

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="dialogService">The dialog service.</param>
        public ViewModelBase(IExtendedNavigation navigationService,
            IPageDialogService dialogService)
        {
            NavigationService = navigationService;
            DialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
        }

        #endregion

        /// <summary>
        ///     Destroys this instance.
        /// </summary>
        public virtual void Destroy()
        {
        }

        /// <summary>
        ///     Called when [navigated from].
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        /// <summary>
        ///     Called when [navigated to].
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        /// <summary>
        ///     Called when [navigating to].
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {
        }

        #endregion
    }
}