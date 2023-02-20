using System;
namespace Test.Core.Services.Interfaces
{
    public interface IValidationService
    {
        bool IsValidEmail(string email);
        bool IsValidPassword(string password);
        bool IsOnlyStringLetters(string value);
        bool IsValidStringPhone(string value);
        bool IsValidStringNotCharacterSpecial(string value);
    }
}

