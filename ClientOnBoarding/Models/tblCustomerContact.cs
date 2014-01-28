using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClientOnBoarding.Models
{
    public class tblCustomerContact
    {

        public int ContactID { get; set; }
        public int CustomerID { get; set; }
        public ContactType ContactType { get; set; }
        public string ExtNofirst { get; set; }
        public string ExtNosecond { get; set; }
        [Required(ErrorMessage = "Please Enter Contact Name")]
        public String ContactName { get; set; }
         [RegularExpression(".+@.+\\..+", ErrorMessage = "Please Enter Correct Email")]
        public String Email { get; set; }
        //[RegularExpression("[0-9]+", ErrorMessage = "Please Enter Correct PhoneNo")]
        public String FirstPhoneNo { get; set; }       
        public String SecondPhoneNo { get; set; }
        public String SMS { get; set; }
        public int IsActive { get; set; }

    }

}