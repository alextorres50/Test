using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Test.Modules.Repositories.Interfaces
{
	public interface IAuthRepository
	{
        Task<bool> Login(string _email = null, string _password = null);
        List<Core.Models.WebApi.Error> GetErrors { get; }
    }
}

