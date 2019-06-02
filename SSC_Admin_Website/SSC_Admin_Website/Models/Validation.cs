using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SSC_Admin_Website.Models
{
    public class Validation
    {
        public static bool StringIsInvalid(string s)
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
            {
                return true;
            }
            return false;
        }

        public static bool StringIsNumber(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsNumber(c))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool StringIsPass(string s)
        {
            var regex = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,25}$");
            return regex.IsMatch(s);
        }
        public static bool StringIsCMND(string s)
        {
            var regex = new Regex(@"^(\d{9}|\d{12})$");
            return regex.IsMatch(s);
        }

        public static bool StringIsEmail(string s)
        {
            var regex = new Regex(@"\w+@\w+\.\w+");
            return regex.IsMatch(s);
        }

        public static bool StringIsPhoneNumber(string s)
        {
            string pattern = "";
            string[] dauSoDiDong = { "070", "079", "077", "076", "078", "089", "090", "093", "083", "084", "085", "081", "082", "088", "091", "094", "032", "033", "034", "035", "036", "037", "038", "039", "086", "096", "097", "098", "056", "058", "092", "059", "099" };
            for (int i = 0; i < dauSoDiDong.Length; i++)
            {
                pattern += string.Format(@"{0}\d", dauSoDiDong[i]);
                pattern += "{7}";
                if (i != dauSoDiDong.Length - 1)
                {
                    pattern += "|";
                }

            }

            var regex = new Regex(pattern);
            return regex.IsMatch(s);
        }
       
    }
}