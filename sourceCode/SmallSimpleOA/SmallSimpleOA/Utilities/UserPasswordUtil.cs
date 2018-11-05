using System;
using System.Text;
namespace SmallSimpleOA.Utilities
{
    public class UserPasswordUtil
    {
        public UserPasswordUtil()
        {
        }

        public static string GenerateSalt()
        {
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                int r = rnd.Next(97, 123); // from a ~ z in ascii
                sb.Append((char)r);
            }
            return sb.ToString();
        }

        public static string GeneratePasswordAfterSalt(string p, string salt)
        {
            return MD5Util.MD5Value(p + salt);
        }
    }
}
