using System;
using System.Data.SqlTypes;
using System.IO;

namespace Test.Core.Constants
{
	public static class Constants
	{
        public static string ApiBaseUrl = $"https://usersignin.free.beeceptor.com/";

        public const string DatabaseFilename = "TestSQLite.db3";

        public const string EMAIL_PATTERN = @"^([a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";

        public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
    }
}

