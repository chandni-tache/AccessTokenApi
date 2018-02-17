using System.Security.Cryptography;
using System.Text;

namespace TachTechnologies.DataAccessLayer.BusinessUtil
{
    public class BusinessUtils
    {
        /// <summary>
        /// GetUniqueKey: to b
        /// </summary>
        /// <param name="maxSize"></param>
        /// <returns></returns>
        public static string GetUniqueKey(int maxSize = 500)
        {
            char[] chars = new char[62];
            chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}