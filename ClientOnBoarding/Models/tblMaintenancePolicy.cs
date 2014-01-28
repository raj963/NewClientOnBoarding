using System;
using System.Collections.Generic;


using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ClientOnBoarding.Models
{
    public class tblMaintenancePolicy
    {
       
    
        public int MaintenancePolicyID { get; set; }
        public int CustomerID { get; set; }
        [Required(ErrorMessage = "Please Enter Activity Name")]
        public string ActivityName { get; set; }
        public ScheduleTypeMaintenancePolicy ScheduleType { get; set; }
        public WeekOfDay WeekOfDays { get; set; }
        public MonthOfDay MonthOfDays { get; set; }
      
        public string   ScheduledStartTime { get; set; }
        public string ScheduledEndTime { get; set; }        
        public DateTime ScheduledStartDate { get; set; }
        public TimeZoneFX TimeZone { get; set; }
        public int WeekOfMonth { get; set; }
    }
}