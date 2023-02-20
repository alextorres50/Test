using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Test.Core.Services.Interfaces;

namespace Test.Core.Services
{
	public class ValidationService : IValidationService
    {
        private readonly ISettingsService _settingsService;
        private ITranslatorService _translatorService;
        public ValidationService(ISettingsService settingsService, ITranslatorService translatorService)
        {
            _settingsService = settingsService;
            _translatorService = translatorService;
        }
        /// <summary>
        ///     Check whether an email address is valid or not
        /// </summary>
        /// <param name="email">Email address to validate.</param>
        /// <returns>whether the email address is valid or not.</returns>
        public bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,7})+)$");

            return !string.IsNullOrWhiteSpace(email) && emailRegex.IsMatch(email);
        }
        /// <summary>
        ///     Check whether a password address is valid or not
        /// </summary>
        /// <param name="password">Password to validate.</param>
        /// <returns>whether the password is valid or not.</returns>
        public bool IsValidPassword(string password)
        {
            var score = 0;

            if (password.Length >= 8)
                score++;
            if (Regex.Match(password, @"\d+").Success)
                score++;
            if (Regex.Match(password, @"[a-z]+").Success
                  && Regex.Match(password, @"[A-Z]+").Success)
                score++;
            if (Regex.Match(password, @"\W+").Success)
                score++;

            return score >= 4;

        }

        /// <summary>
        /// Use to Check Only String Letters.
        /// </summary>
        /// <returns><c>true</c>, if valid first name last name was used, <c>false</c> otherwise.</returns>
        /// <param name="value">Value.</param>
        public bool IsOnlyStringLetters(string value)
        {
            var result = Regex.IsMatch(value, @"^[a-zA-Z àáéèêíìóòúùçñ\s\p{L}][a-zA-Z àáéèêíìóòúùçñ\s\p{L}]+$");
            return result;
        }
        /// <summary>
        /// Uses the valid string numbers.
        /// </summary>
        /// <returns><c>true</c>, if valid string numbers was ised, <c>false</c> otherwise.</returns>
        /// <param name="value">Value.</param>
        public bool IsValidStringPhone(string value)
        {
            return Regex.IsMatch(value, @"^\d+$") && !value.Contains(".") && !value.Contains(",");
        }
        /// <summary>
        /// Use to check if string haven`t Character Special
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsValidStringNotCharacterSpecial(string value)
        {
            Regex rgx = new Regex("^[^`~!@#$%^&*()_+={}\\[\\]|\\\\:;“’<,>.?๐฿\\-]*$");
            return rgx.IsMatch(value);
        }
        
    }
}

