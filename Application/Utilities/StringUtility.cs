using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utilities
{
    public class StringUtility : IStringUtility
    {
        public string HashString(string str)
        {
            using (var sha256 = SHA256.Create())
            {
                // Convert the password string to bytes
                byte[] stringBytes = Encoding.UTF8.GetBytes(str);

                // Compute the hash
                byte[] hashedBytes = sha256.ComputeHash(stringBytes);

                // Convert the hashed bytes back to a string
                string hashedString = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                return hashedString;
            }
        }

        public string? NicenName(string? fName, string? lName)
        {
            return fName + " " + lName;
        }

        public bool VerifyEquailityForTwoPasswords(string str1, string str2)
        {
            string hashedInputPassword = HashString(str2);
            return string.Equals(hashedInputPassword, str1, StringComparison.OrdinalIgnoreCase);
        }
    }
}
