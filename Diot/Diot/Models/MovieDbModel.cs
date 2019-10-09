using System.Collections.Generic;
using System.Dynamic;
using SQLite;
using Xamarin.Forms;

namespace Diot.Models
{
    [Table("Movies")]
    public class MovieDbModel
    {
        #region Fields

        private ImageSource _coverImage = "library_icon.png";

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the vote count.
        /// </summary>
        public int Vote_Count { get; set; }

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        [PrimaryKey]
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="MovieDbModel" /> is video.
        /// </summary>
        public bool Video { get; set; }

        /// <summary>
        ///     Gets or sets the vote average.
        /// </summary>
        public double Vote_Average { get; set; }

        /// <summary>
        ///     Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     Gets or sets the popularity.
        /// </summary>
        public double Popularity { get; set; }

        /// <summary>
        ///     Gets or sets the poster path.
        /// </summary>
        public string Poster_Path { get; set; }

        /// <summary>
        ///     Gets or sets the cover image.
        /// </summary>
        [Ignore]
        public ImageSource CoverImage
        {
            get => _coverImage;
            set => _coverImage = value;
        }

        /// <summary>
        ///     Gets or sets the cover image byte array. Used for caching the cover image.
        /// </summary>
        /// <value>
        /// The cover image byte array.
        /// </value>
        public byte[] CoverImageByteArray { get; set; }
    

        /// <summary>
        ///     Gets or sets the original language.
        /// </summary>
        public string Original_Language { get; set; }

        /// <summary>
        ///     Gets or sets the original title.
        /// </summary>
        public string Original_Title { get; set; }

        /// <summary>
        ///     Gets or sets the genre ids.
        /// </summary>
        [Ignore]
        public List<int> Genre_Ids { get; set; }

        /// <summary>
        ///     Gets or sets the backdrop path.
        /// </summary>
        public string Backdrop_Path { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="MovieDbModel" /> is adult themed.
        /// </summary>
        public bool Adult { get; set; }

        /// <summary>
        ///     Gets or sets the overview.
        /// </summary>
        public string Overview { get; set; }

        /// <summary>
        ///     Gets or sets the release date.
        /// </summary>
        public string Release_Date { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is on DVD.
        /// </summary>
        public bool IsOnDvd { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is on bluray.
        /// </summary>
        public bool IsOnBluray { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is on uhd.
        /// </summary>
        public bool IsOnUhd { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is on vudu.
        /// </summary>
        public bool IsOnVudu { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is on movies anywhere.
        /// </summary>
        public bool IsOnMoviesAnywhere { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is on plex.
        /// </summary>
        public bool IsOnPlex { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is on amazon.
        /// </summary>
        public bool IsOnAmazon { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is on other.
        /// </summary>
        public bool IsOnOther { get; set; }

        /// <summary>
        ///     Gets or sets comment when format is other.
        /// </summary>
        public string OtherComment { get; set; }

        #endregion
    }
}