

using System;
using Xamarin.Forms;
using System.IO;
using Diot.Droid.Implementations;
using Diot.Interface;
using SQLite;

[assembly: Dependency(typeof(DatabaseController))]
namespace Diot.Droid.Implementations
{
    public class DatabaseController : IDatabaseController
    {
        #region Methods

        /// <summary>
        ///     Returns a sqlite database connection.
        /// </summary>
        public SQLiteConnection GetConnection()
        {
            const string sqliteFileName = "libraryDB.db3";
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFileName);
            var conn = new SQLiteConnection(path);

            return conn;
        }

        #endregion
    }
}