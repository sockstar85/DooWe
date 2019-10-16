using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Diot.Interface
{
    /// <summary>
    ///     Interface for the loading page service.
    /// </summary>
    public interface ILoadingPageService
    {
        #region Methods

        /// <summary>
        ///     Shows the loading page.
        /// </summary>
        /// <param name="loadingPageText">The loading page text.</param>
        /// <param name="delay">The delay.</param>
        /// <param name="callerName">Name of the caller.</param>
        void ShowLoadingPage(string loadingPageText, int delay = 0, [CallerMemberName] string callerName = null);

        /// <summary>
        ///     Hides the loading page.
        /// </summary>
        /// <param name="delay">The delay.</param>
        /// <param name="callername">The callername.</param>
        Task HideLoadingPageAsync(int delay = 0, [CallerMemberName] string callername = null);

        #endregion
    }
}
