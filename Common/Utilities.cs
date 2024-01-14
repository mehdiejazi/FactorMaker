using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public class Utilities
    {
        public static string HashSHA1(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        public static bool IsValidNationalCode(string meli)
        {
            try
            {
                int a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a, b;
                int sum, mm;
                char[] code = new char[12];
                if (meli == "1111111111" || meli == "2222222222" ||
                    meli == "3333333333" || meli == "4444444444" ||
                    meli == "5555555555" || meli == "6666666666" ||
                    meli == "7777777777" || meli == "8888888888" ||
                    meli == "9999999999" || meli == "0000000000")
                    return false;
                code = meli.ToCharArray();
                ///////////////////////////////
                a1 = Convert.ToInt32(code[0].ToString()) * 10;//سه رقم اول
                a2 = Convert.ToInt32(code[1].ToString()) * 9;//
                a3 = Convert.ToInt32(code[2].ToString()) * 8;//
                                                             //-----------------------
                a4 = Convert.ToInt32(code[3].ToString()) * 7;//
                a5 = Convert.ToInt32(code[4].ToString()) * 6;//
                a6 = Convert.ToInt32(code[5].ToString()) * 5;//شش رقم میانی
                a7 = Convert.ToInt32(code[6].ToString()) * 4;//
                a8 = Convert.ToInt32(code[7].ToString()) * 3;//
                a9 = Convert.ToInt32(code[8].ToString()) * 2;//
                                                             //------------------
                a10 = Convert.ToInt32(code[9].ToString());//رقم اخر

                sum = a1 + a2 + a3 + a4 + a5 + a6 + a7 + a8 + a9;
                mm = (sum % 11);
                if (mm < 2)
                    if (a10 == mm)
                        return true;
                if (mm >= 2)
                    if ((11 - mm) == a10)
                        return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
