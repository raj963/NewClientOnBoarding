using System;
using System.Data;
using System.Net.Mail;
using System.Security.Cryptography;

namespace ClientOnBoarding
{
    public static class Common
    {
        private const string USER_KEY = "PlAf";

        public static  string GetNewRandonNumber()
        {
            var rng = new Random();
            return rng.Next(1000, 9999).ToString();
        }

        public static string GetNewSpecialCharacter()
        {
            Random _rng = new Random();
            string _chars = "*$?!%";

            char[] buffer = new char[2];

            for (int i = 0; i < 2; i++)
            {
                buffer[i] = _chars[_rng.Next(_chars.Length)];
            }
            return new string(buffer);
        }

        public static void SendMail(string toEmail, string subject, string mailContent)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    //HOST
                    string host = AppSettings.HostName;

                    //PORT
                    int port = Convert.ToInt32(AppSettings.SMTPport);

                    //USER NAME
                    string userName = AppSettings.SMTPUserName;

                    //PASSWORD
                    string password = AppSettings.SMTPPassword;

                    //Enable Ssl
                    string enableSsl = AppSettings.EnableSsl;


                    //FROM MAIL
                    string fromMail = AppSettings.FromMail;
                    MailAddress MailFrom = new MailAddress(fromMail);
                    mail.From = MailFrom;

                    //TO
                    string[] strToAdress = toEmail.Split(new string[] { ";" }, StringSplitOptions.None);
                    for (int i = 0; i < strToAdress.Length; i++)
                    {
                        if (Convert.ToString(strToAdress[i]).Trim() != "")
                        {
                            MailAddress MailTo = new MailAddress(strToAdress[i].Trim());
                            mail.To.Add(MailTo);
                        }
                    }

                    //SUBJECT
                    mail.Subject = subject;

                    //BODY
                    mail.Body = mailContent;
                    mail.IsBodyHtml = true;

                    //SMTP
                    SmtpClient smtp = new SmtpClient { Host = host, Port = port };
                    smtp.Credentials = new System.Net.NetworkCredential(userName, password);
                    smtp.EnableSsl = enableSsl == "N" ? false : true;
                    smtp.Send(mail);
                }

            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        public static string ConvertToString(DataRow objRow, string colmnName)
        {
            if (objRow != null && objRow.Table.Columns.Contains(colmnName))
            {
                if (objRow[colmnName] == null)
                    return "";
                else if (objRow[colmnName].GetType() == typeof(System.Decimal))
                {
                    decimal decVal = 0;
                    decimal.TryParse(objRow[colmnName].ToString(), out decVal);
                    return decVal.ToString("N2");
                }
                else if (objRow[colmnName].GetType() == typeof(System.DateTime))
                {
                    DateTime dtVal = new DateTime();
                    DateTime.TryParse(objRow[colmnName].ToString(), out dtVal);
                    if (dtVal.Equals(new DateTime()))
                        return "";

                    return dtVal.ToShortDateString();
                }
                else
                    return objRow[colmnName].ToString().Trim();
            }
            else
                return "";
        }

        public static bool ConvertToBool(DataRow objRow, string colmnName)
        {
            if (objRow.Table.Columns.Contains(colmnName))
            {
                if (objRow[colmnName] == null)
                    return false;
                else
                {
                    bool retVal = false;
                    bool.TryParse(objRow[colmnName].ToString().Trim(), out retVal);
                    return retVal;
                }
            }
            else
                return false;
        }

        public static bool ConvertToBool(string stringValue)
        {
            bool retVal = false;
            bool.TryParse(stringValue, out retVal);
            return retVal;
        }

        public static DateTime ConvertToDateTime(DataRow objRow, string colmnName)
        {
            DateTime DateTimeValue = DateTime.Today;
            DateTime.TryParse(ConvertToString(objRow, colmnName), out DateTimeValue);
            return DateTimeValue;
        }

        public static int ConvertToInt(DataRow objRow, string colmnName)
        {
            int intValue = 0;
            int.TryParse(ConvertToString(objRow, colmnName), out intValue);
            return intValue;
        }

        public static int ConvertToInt(string stringValue)
        {
            int intValue = 0;
            int.TryParse(stringValue, out intValue);
            return intValue;
        }

        public static decimal ConvertToDecimal(DataRow objRow, string colmnName)
        {
            decimal decimalValue = 0;
            decimal.TryParse(ConvertToString(objRow, colmnName), out decimalValue);
            return decimalValue;
        }

        public static decimal ConvertToDecimal(string stringValue)
        {
            decimal decimalValue = 0;
            decimal.TryParse(stringValue, out decimalValue);
            return decimalValue;
        }

        public static double ConvertToDouble(DataRow objRow, string colmnName)
        {
            double doubleValue = 0;
            double.TryParse(ConvertToString(objRow, colmnName), out doubleValue);
            return doubleValue;
        }

        public static string ConvertToShortDateString(DataRow objRow, string colmnName)
        {
            if (objRow.Table.Columns.Contains(colmnName))
            {
                if (objRow[colmnName] == null)
                    return "";
                else
                {
                    DateTime dt;
                    DateTime.TryParse(objRow[colmnName].ToString(), out dt);
                    if (dt.Equals(DateTime.MinValue))
                        return "";
                    else
                        return dt.ToShortDateString();
                }
            }
            else
                return "";
        }

        public static string ValidateStringLength(string fieldValue, int fieldLimit)
        {
            fieldValue = fieldValue.Replace("''", "'");
            fieldValue = fieldValue.Length > fieldLimit ? fieldValue.Substring(0, fieldLimit) : fieldValue;
            fieldValue = fieldValue.Replace("'", "''");
            return fieldValue;
        }

        public static string CreateFormattedTime()
        {
            string hours;
            string minutes;
            string seconds;
            hours = minutes = seconds = string.Empty;

            hours = DateTime.Now.TimeOfDay.Hours < 10 ? "0" + DateTime.Now.TimeOfDay.Hours.ToString() : DateTime.Now.TimeOfDay.Hours.ToString();
            minutes = DateTime.Now.Minute < 10 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();
            seconds = DateTime.Now.Second < 10 ? "0" + DateTime.Now.Second.ToString() : DateTime.Now.Second.ToString();
            return String.Format("{0}:{1}:{2}", hours, minutes, seconds);
        }

        internal static object ConvertToInt(int p)
        {
            throw new NotImplementedException();
        }
    }
}
