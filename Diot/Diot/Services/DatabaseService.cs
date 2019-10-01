using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Diot.Interface;
using Diot.Models;
using SQLite;
using Xamarin.Forms;

namespace Diot.Services
{
    public class DatabaseService
    {
        #region  Fields

        private static readonly object locker = new object();
        private readonly SQLiteConnection conn;

        #endregion

        #region Methods

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DatabaseService" /> class.
        /// </summary>
        public DatabaseService()
        {
            conn = DependencyService.Get<IDatabaseController>().GetConnection();
            lock (locker)
            {
                try
                {
                    conn.CreateTable<MovieDbModel>();
                }
                catch (Exception e)
                {
                    Debug.Write(e.Message);
                }
            }
        }

        #endregion

        /// <summary>
        ///     Gets all movies and returns sorted list.
        /// </summary>
        public List<MovieDbModel> GetAllMovies()
        {
            lock (locker)
            {
                return conn?.Table<MovieDbModel>()?.ToList()?.OrderBy(x => x.Title)?.ToList();
            }
        }

        /// <summary>
        ///     Saves (updates or inserts) a movie.
        /// </summary>
        /// <param name="movie">The movie.</param>
        public int SaveMovie(MovieDbModel movie)
        {
            lock (locker)
            {
                var movieExists = false;

                try
                {
                    var query = $"SELECT * FROM Movies WHERE id={movie.Id};";
                    movieExists = conn.FindWithQuery<MovieDbModel>(query) != null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error : " + ex.Message);
                }

                if (!movieExists)
                {
                    return conn.Insert(movie);
                }

                conn.Update(movie);
                return movie.Id;
            }
        }

        /// <summary>
        ///     Deletes the movie.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public int DeleteMovie(int id)
        {
            lock (locker)
            {
                return conn.Delete(id);
            }
        }

        /// <summary>
        ///     Deletes all movies.
        /// </summary>
        public List<MovieDbModel> DeleteAllMovies()
        {
            lock (locker)
            {
                conn.DropTable<MovieDbModel>();
                conn.CreateTable<MovieDbModel>();
                return conn.Table<MovieDbModel>().ToList();
            }
        }

        #endregion
    }
}