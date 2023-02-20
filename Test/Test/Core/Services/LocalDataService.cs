using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Test.Core.Helpers;
using Test.Core.Services.Interfaces;
using Test.Core.Models.LocalStorage;

namespace Test.Core.Services
{
	public class LocalDataService : ILocalDataService
	{
		public LocalDataService()
		{
            Database = new SQLiteAsyncConnection(Core.Constants.Constants.DatabasePath, Core.Constants.Constants.Flags);
        }

        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<LocalDataService> Instance = new AsyncLazy<LocalDataService>(async () =>
        {
            var instance = new LocalDataService();
            CreateTableResult result = await Database.CreateTableAsync<User>();
            return instance;
        });

        public Task<List<User>> GetItemsAsync()
        {
            return Database.Table<User>().ToListAsync();
        }

        public Task<User> GetItemAsync(int id)
        {
            return Database.Table<User>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(User item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(User item)
        {
            return Database.DeleteAsync(item);
        }
    }
}

