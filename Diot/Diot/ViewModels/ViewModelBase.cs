using System;
using Diot.Interface;
using Diot.Interface.ViewModels;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace Diot.ViewModels
{
    /// <summary>
    ///     The base to all view models.
    /// </summary>
    /// <seealso cref="BindableBase" />
    /// <seealso cref="INavigationAware" />
    /// <seealso cref="IDestructible" />
    public class ViewModelBase : BindableBase, IViewModelBase
    {
        #region  Fields

        private string _title;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the navigation service.
        /// </summary>
        public IExtendedNavigation NavigationService { get; }

        /// <summary>
        ///     Gets the dialog service.
        /// </summary>
        public IPageDialogService DialogService { get; }

        /// <summary>
        ///     Gets or sets the title.
        /// </summary>
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        /// <summary>
        ///     Gets the loading page service.
        /// </summary>
        public ILoadingPageService LoadingPageService { get; }

        #endregion

        #region Methods

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        /// <param name="navigationService">The navigation service.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="loadingPageService">The loading page service.</param>
        /// <exception cref="ArgumentNullException">dialogService</exception>
        public ViewModelBase(IExtendedNavigation navigationService,
            IPageDialogService dialogService,
            ILoadingPageService loadingPageService)
        {
            LoadingPageService = loadingPageService;
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