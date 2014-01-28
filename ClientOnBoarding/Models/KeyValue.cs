using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientOnBoarding.Models
{
    public class KeyValue
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public static IEnumerable<KeyValue> GetErrorsFromModelState(ViewDataDictionary ViewData)
        {
            List<KeyValue> keyValue = new List<KeyValue>();

            foreach (var item in ViewData.ModelState)
            {
                if (item.Value.Errors.Any())
                {
                    KeyValue keyVal = new KeyValue();
                    keyVal.Key = item.Key;
                    keyVal.Value = item.Value.Errors[0].ErrorMessage;
                    keyValue.Add(keyVal);
                }
            }

            return keyValue;
        }
    }
}