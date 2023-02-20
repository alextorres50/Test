using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Core.Models.LocalStorage;

namespace Test.Core.Services.Interfaces
{
	public interface ILocalDataService
	{
        Task<List<User>> GetItemsAsync();
        Task<User> GetItemAsync(int id);
        Task<int> SaveItemAsync(User item);
        Task<int> DeleteItemAsync(User item);

    }
}

