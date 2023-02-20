using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Core.Models.WebApi.Contacts;

namespace Test.Modules.Repositories.Interfaces
{
	public interface IContactRepository
	{
        Task<List<Contact>> GetContacts();
        List<Core.Models.WebApi.Error> GetErrors { get; }

    }
}

