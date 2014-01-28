using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClientOnBoarding.Models
{
    public class tblClientSite
    {
        public int ClientID { get; set; }
        public int CustomerID { get; set; }
        [Required(ErrorMessage = "Please Enter Business Name")]
        public string BusinessName { get; set; }
        public ServiceType ServiceType { get; set; }
        public TimeZoneFX TimeZone { get; set; }
        public SetUpStaus Status { get; set; }
        public int AccessPolicyID { get; set; }
        public int IsActive { get; set; }
        public PolicyType AccessPolicyType { get; set; }

    }
   
}