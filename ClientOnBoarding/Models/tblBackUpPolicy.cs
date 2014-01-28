using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClientOnBoarding.Models
{
    public class tblBackUpPolicy
    {
        public int BackUpPolicyID { get; set; }
        public int CustomerID { get; set; }
        [Required(ErrorMessage = "Please Enter Policy Name")]
        public string PolicyName { get; set; }
        public string ProductName { get; set; }
        public string VolumeLocation { get; set; }
        public string FolderLocation { get; set; }
        public TimeZoneFX ScheduleTimeZone { get; set; }
        public string BackUpSetDetails { get; set; }
        [RegularExpression("[0-9]+", ErrorMessage = "Please Enter Correct Number")]
        public int DifferentialEveryDay { get; set; }
        [RegularExpression("[0-9]+", ErrorMessage = "Please Enter Correct Number")]
        public int PreviousBackupSaved { get; set; }
        [RegularExpression("[0-9]+", ErrorMessage = "Please Enter Correct Number")]
        public int FullBackUpEveryDay { get; set; }
        public string DomainName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public PolicyType PatchingPolicyType { get; set; }
        public string ScheduleTime { get; set; }
    }

}