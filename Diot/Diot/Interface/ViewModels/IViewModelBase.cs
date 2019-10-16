using Prism.Navigation;
using Prism.Services;

namespace Diot.Interface.ViewModels
{
    /// <summary>
    ///     Interface of the base view model.
    /// </summary>
    /// <seealso cref="INavigationAware" />
    /// <seealso cref="IDestructible" />
    public interface IViewModelBase : INavigationAware, IDestructible
    {
        #region Properties

        /// <summary>
        ///     Gets the navigation service.
        /// </summary>
        IExtendedNavigation NavigationService { get; }

        /// <summary>
        ///     Gets the dialog service.
        /// </summary>
        IPageDialogService DialogService { get; }

        /// <summary>
        ///     Gets or sets the title.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        ///     Gets the loading page service.
        /// </summary>
        ILoadingPageService LoadingPageService { get; }

        #endregion
    }
}
