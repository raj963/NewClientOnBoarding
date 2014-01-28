using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClientOnBoarding.Models
{
    public class tblAntiVirusPolicy
    {
        public int PatchingPolicyID { get; set; }
        public PolicyType PatchingPolicyType { get; set; }
        public int AntiVirusID { get; set; }
        public int CustomerID { get; set; }
        [Required(ErrorMessage = "Please Enter Policy Name")]
        public string PolicyName { get; set; }
        public string ProductName { get; set; }
        public int SetupStatus { get; set; }
        public string PatchingTime { get; set; }
        public TimeZoneFX PatchingTimeZone { get; set; }
        public ScheduleType ScheduleType { get; set; }
        public WeekOfDay WeekOfDay { get; set; }
        public MonthOfDay MonthOfDay { get; set; }
        public string ExcludedFilesExtension { get; set; }
        public string ExcludedFileTypes { get; set; }
        public string ExcludedFilePaths { get; set; }

    }

   
}