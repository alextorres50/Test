using System;
using SQLite;

namespace Test.Core.Models.LocalStorage
{
	public class User
	{
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public DateTime Date { get; set; }
    }
}

