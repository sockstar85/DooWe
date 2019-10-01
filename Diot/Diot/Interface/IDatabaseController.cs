using SQLite;

namespace Diot.Interface
{
    public interface IDatabaseController
    {
        #region Methods

        /// <summary>
        ///     Returns a sqlite database connection.
        /// </summary>
        SQLiteConnection GetConnection();

        #endregion
    }
}