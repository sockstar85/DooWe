using System;
using System.Threading.Tasks;
using Prism.Navigation;

namespace Diot.Interface
{
    public interface IExtendedNavigation
    {
        #region Methods

        Task<INavigationResult> GoBackAsync(INavigationParameters parameters = null, bool animated = true,
            bool useModal = false);

        Task<INavigationResult> NavigateAsync(Uri uri, INavigationParameters parameters = null,
            bool useModal = false, bool animated = true);

        Task<INavigationResult> NavigateAsync(string name, INavigationParameters parameters = null,
            bool useModal = false, bool animated = true);

        #endregion
    }
}