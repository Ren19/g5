using System.Text.RegularExpressions;

namespace G5.Utility.Extensions
{
    public static class RegexValidation
    {
        public static bool EmailFormatValidation(string email)
        {
            if (email == "")
                return true;
            return Regex.IsMatch(email,
                  @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                  @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");
        }

        public static bool AlphaNumericFormatValidation(string value)
        {
            if (value == "")
                return true;
            return Regex.IsMatch(value, "^[a-zA-Z0-9]*$");
        }

        public static bool AlphabeticFormatWithSpacesValidation(string value)
        {
            if (value == "")
                return true;
            return Regex.IsMatch(value, "^[a-zA-ZÁÉÍÓÚáéíóúäëïöüÄËÏÖÜ ]*$");
        }

        public static bool AlphabeticWithSpecialCharactersFormatValidation(string value)
        {
            if (value == "")
                return true;
            return Regex.IsMatch(value, "^[ñA-Za-zÁÉÍÓÚáéíóúäëïöüÄËÏÖÜ _]*[ñA-Za-zÁÉÍÓÚáéíóúäëïöüÄËÏÖÜ][ñA-Za-zÁÉÍÓÚáéíóúäëïöüÄËÏÖÜ _]*$");
        }

        public static bool NumericFormatValidation(string value)
        {
            if (value == "")
                return true;
            return Regex.IsMatch(value, "^[0-9]*$");
        }

        public static bool NumericPhoneFormatValidation(string value)
        {
            if (value == "")
                return true;
            return Regex.IsMatch(value, "^[0-9()+]*$");
        }
    }
}
