using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Functions
{
    public class UserFunctions
    {
        public string GenerateEmployeeNumber(string empNo)
        {
            if (empNo.Length < 4)
            {
                while (empNo.Length < 4)
                {
                    empNo = "0" + empNo;
                }
            }

            return "EMP" + empNo;
        }

        public bool IsValueNullOrEmpty(string value)
        {
            if (value == null || value == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsValidSLIITEmail(string email)
        {
            if (email.EndsWith("@sliit.lk"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string ExtractNumbers(string str)
        {
            string strVal = string.Empty;

            for(int i = 0; i < str.Length; i++)
            {
                if (Char.IsDigit(str[i]))
                {
                    strVal += str[i];
                }
            }

            return strVal;
        }
    }
}