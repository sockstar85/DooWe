using System.Collections.Generic;
using System.Windows.Input;
using Diot.Models;

namespace Diot.Interface.ViewModels
{
    public interface IMainPageViewModel : IViewModelBase
    {
        #region Properties

        /// <summary>
        ///     Gets the navigate to add new page command.
        /// </summary>
        ICommand NavigateToAddNewPageCommand { get; }

        /// <summary>
        ///     Gets the navigate to movie details command.
        /// </summary>
        ICommand NavigateToMovieDetailsCommand { get; }

        /// <summary>
        ///     Gets or sets the movies list.
        /// </summary>
        List<MovieDbModel> MoviesList { get; set; }

        #endregion
    }
}
