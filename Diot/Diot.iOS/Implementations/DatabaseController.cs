using System;
using System.IO;
using Diot.iOS.Implementations;
using Diot.Interface;
using Prism;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(DatabaseController))]

namespace Diot.iOS.Implementations
{
    public class DatabaseController : IDatabaseController
    {
        #region Methods

        /// <summary>
        ///     Returns a sqlite database connection.
        /// </summary>
        public SQLiteConnection GetConnection()
        {
            var fileName = PrismApplicationBase.Current.Resources["DbName"].ToString();
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentPath, "..", "Library");
            var path = Path.Combine(libraryPath, fileName);
            var connection = new SQLiteConnection(path);

            return connection;
        }

        #endregion
    }
}