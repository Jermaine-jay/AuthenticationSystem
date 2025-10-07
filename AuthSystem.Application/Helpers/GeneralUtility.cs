using System.Text;

namespace AuthSystem.Application.Helpers
{
    public static class GeneralUtility
    {
        public static string GenerateRandomString(int length, string id = "")
        {
            string capitalAlphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string smallAlphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = numbers + capitalAlphabets + smallAlphabets + id;
            string password = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (password.IndexOf(character) != -1);
                password += character;
            }
            return password;
        }

        public static string GenerateUniqueToken(string userId)
        {
            StringBuilder build = new StringBuilder();
            Encoding encoding = Encoding.ASCII;
            Encoding enc = Encoding.UTF8;

            var user = Convert.ToBase64String(encoding.GetBytes(userId));
            var ops = Convert.ToBase64String(encoding.GetBytes(GenerateRandomString(36)));
            var extra = Convert.ToBase64String(enc.GetBytes(GenerateRandomString(36)));

            build.Append(ops);
            build.Append(user);
            build.Append(extra);

            return build.ToString();
        }

        public static string DecodeUniqueToken(string validToken)
        {
            string getstring = validToken.Substring(48, validToken.Length - 96);
            var getUserId = Convert.FromBase64String(getstring);
            string decodedStr = Encoding.ASCII.GetString(getUserId);

            return decodedStr;
        }
    }
}
