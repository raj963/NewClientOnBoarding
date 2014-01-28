using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientOnBoarding.Models
{
    public class tblCustomerDetails
    {
        public int CustomerID { get; set; }
        [RegularExpression(".+@.+\\..+", ErrorMessage = "Please Enter Correct Email")]
        [Required(ErrorMessage = "Please Enter Email address Name")]
        [Remote("JsonCheckEmail", "ManageUser", ErrorMessage = "This Email Address has already been registered.")]
        public String EmailAddress { get; set; }
        [Required(ErrorMessage = "Please Enter password")]
        public String Password { get; set; }
        [Required(ErrorMessage = "Please Enter Customer Name")]
        public String CustomerName { get; set; }
        public TimeZoneFX TimeZone { get; set; }
        public String CustomerContactName { get; set; }
        public NOCCommunicationBy NOCCommunication { get; set; }
        public int RoleID { get; set; }
        public int IsActive { get; set; }
    }

    public struct UserRole
    {
        public const int SuperAdmin = 1;
        public const int Admin = 2;
        public const int NormalUser = 3;
        public const int Staff = 4;
    }
}