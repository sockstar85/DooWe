using System;
using System.Threading.Tasks;
using Diot.Interface;
using Prism.Navigation;

namespace Diot.Services
{
    public class ExtendedNavigation : IExtendedNavigation
    {
        #region  Fields

        private readonly INavigationService _navigationService;

        #endregion

        #region Methods

        #region Constructors

        public ExtendedNavigation(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        #endregion

        /// <summary>
        ///     Goes the back asynchronous.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="animated">if set to <c>true</c> [animated].</param>
        /// <param name="useModal">if set to <c>true</c> [use modal].</param>
        /// <returns></returns>
        public async Task<INavigationResult> GoBackAsync(INavigationParameters parameters = null, bool animated = true,
            bool useModal = false)
        {
            return await _navigationService.GoBackAsync(parameters, useModal, animated);
        }

        /// <summary>
        ///     Navigates the asynchronous.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="useModal">if set to <c>true</c> [use modal].</param>
        /// <param name="animated">if set to <c>true</c> [animated].</param>
        /// <returns></returns>
        public async Task<INavigationResult> NavigateAsync(Uri uri, INavigationParameters parameters = null,
            bool useModal = false, bool animated = true)
        {
            return await _navigationService.NavigateAsync(uri, parameters, useModal, animated);
        }

        /// <summary>
        ///     Navigates the asynchronous.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="useModal">if set to <c>true</c> [use modal].</param>
        /// <param name="animated">if set to <c>true</c> [animated].</param>
        /// <returns></returns>
        public async Task<INavigationResult> NavigateAsync(string name, INavigationParameters parameters = null,
            bool useModal = false, bool animated = true)
        {
            return await _navigationService.NavigateAsync(name, parameters, useModal, animated);
        }

        #endregion
    }
}