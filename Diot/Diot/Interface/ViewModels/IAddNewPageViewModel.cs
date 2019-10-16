using Prism.Navigation;
using System.Collections.Generic;
using System.Windows.Input;

namespace Diot.Interface.ViewModels
{
    public interface IAddNewPageViewModel : INavigationAware, IDestructible
    {
        #region Properties

        /// <summary>
        ///     Gets the add selected movies command.
        /// </summary>
        ICommand AddSelectedMoviesCommand { get; }

        /// <summary>
        ///     Gets the search movie command.
        /// </summary>
        ICommand SearchMovieCommand { get; }

        /// <summary>
        ///     Gets the select deselect movie command.
        /// </summary>
        ICommand SelectDeselectMovieCommand { get; }

        /// <summary>
        ///     Gets or sets the movie title.
        /// </summary>
        string MovieTitle { get; set; }

        /// <summary>
        ///     Gets or sets the search results.
        /// </summary>
        List<ISelectableMovieViewModel> SearchResults { get; set; }

        #endregion
    }
}
