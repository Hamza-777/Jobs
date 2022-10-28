using System.Security.Cryptography;
using System.Text;

namespace JobsAPI.Hashing
{
    public class HashMethods
    {
        public string GenerateSalt()
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            return Convert.ToBase64String(salt);
        }
        public byte[] GetHash(string PlainPassword,string Salt)
        {
            byte[] byteArray = Encoding.Unicode.GetBytes(String.Concat(Salt, PlainPassword));
            SHA256Managed sha256 = new SHA256Managed();
            byte[] hashedBytes = sha256.ComputeHash(byteArray);
            return hashedBytes;
        }

        public bool CompareHashedPasswords(string UserInputPassword,string ExistingHashedBase64StringPassword,string Salt)
        {
            
            string UserInputtedHashedPassword = Convert.ToBase64String(GetHash(UserInputPassword, Salt));
            return ExistingHashedBase64StringPassword == UserInputtedHashedPassword;
        }
    }
}
