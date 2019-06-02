﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSC_Admin_Website.Models
{
    public class EncodeBase64
    {
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}