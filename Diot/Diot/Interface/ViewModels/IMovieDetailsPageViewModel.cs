using Diot.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace Diot.Interface.ViewModels
{
    public interface IMovieDetailsPageViewModel : IViewModelBase
    {
		#region Properties

		/// <summary>
		///		Gets the refresh formats command.
		/// </summary>
		ICommand RefreshFormatsCommand { get; }

		/// <summary>
		///		Gets the edit movie command.
		/// </summary>
		ICommand EditMovieCommand { get; }

        /// <summary>
        ///     Gets the delete movie command.
        /// </summary>
        ICommand DeleteMovieCommand { get; }

        /// <summary>
        ///     Gets or sets the selected movie.
        /// </summary>
        MovieDbModel SelectedMovie { get; set; }

        /// <summary>
        ///     Gets or sets the cover image.
        /// </summary>
        ImageSource CoverImage { get; set; }

        #endregion
    }
}
