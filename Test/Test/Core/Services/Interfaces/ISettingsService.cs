using System;
namespace Test.Core.Services.Interfaces
{
    public interface ISettingsService
    {
        void DeleteItem(string id);
        string GetString<T>(string id);
        void SetString<T>(string id, string data);
        int GetInt<T>(string id);
        void SetInt<T>(string id, int data);
        bool GetBool<T>(string id);
        void SetBool<T>(string id, bool data);
    }
}

