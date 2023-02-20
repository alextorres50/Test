using System;
using System.Runtime;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Test.Core.Services.Interfaces;

namespace Test.Core.Services
{
	public class SettingsService : ISettingsService
    {
        private readonly ISettings _appSettings;


        /// <summary>
        ///     Constructor
        /// </summary>
        public SettingsService()
        {
            _appSettings = CrossSettings.Current;
        }

        /// <summary>
        ///     Remove an item with a given id from local settings storage
        /// </summary>
        /// <param name="id">Id of the setting to be removed</param>
        public void DeleteItem(string id)
        {
            _appSettings.Remove(id);
        }

        /// <summary>
        /// Get a string with a given id from the local settings storage
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetString<T>(string id)
        {
            return _appSettings.GetValueOrDefault(id, string.Empty);
        }

        /// <summary>
        ///     Store an item with a given id from the local storage
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the item to be stored (String)
        /// </typeparam>
        /// <param name="id">id of the setting to be stored</param>
        /// <param name="data">Value of the setting to be stored</param>
        public void SetString<T>(string id, string data)
        {

            _appSettings.AddOrUpdateValue(id, data);
        }

        /// <summary>
        /// Get a int with a given id from the local settings storage
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetInt<T>(string id)
        {
            return _appSettings.GetValueOrDefault(id, 0);
        }

        /// <summary>
        ///     Store an item with a given id from the local storage
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the item to be stored ( Int32, Int64)
        /// </typeparam>
        /// <param name="id">id of the setting to be stored</param>
        /// <param name="data">Value of the setting to be stored</param>
        public void SetInt<T>(string id, int data)
        {

            _appSettings.AddOrUpdateValue(id, data);
        }

        /// <summary>
        /// Get a bool with a given id from the local settings storage
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool GetBool<T>(string id)
        {
            return _appSettings.GetValueOrDefault(id, false);
        }

        /// <summary>
        ///     Store an item with a given id from the local storage
        /// </summary>
        /// <typeparam name="T">
        ///     Type of the item to be stored ( Int32, Int64)
        /// </typeparam>
        /// <param name="id">id of the setting to be stored</param>
        /// <param name="data">Value of the setting to be stored</param>
        public void SetBool<T>(string id, bool data)
        {

            _appSettings.AddOrUpdateValue(id, data);
        }

    }
}

