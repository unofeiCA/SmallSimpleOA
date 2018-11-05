using System;
using System.Security.Cryptography;
using System.Text;

namespace SmallSimpleOA.Utilities
{
    public class MD5Util
    {
        public MD5Util()
        {
        }

        public static string MD5Value(string s)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] md5Bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(s));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < md5Bytes.Length; i++)
            {
                sBuilder.Append(md5Bytes[i].ToString("x2"));
            }
            string res = sBuilder.ToString();
            return res;
        }
    }
}
