using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClientOnBoarding.Models
{
    public class tblPatchingPolicy
    {
      // public int PatchingPolicyID { get; set; }
        public int CustomerID { get; set; }
       // public PolicyType PatchingPolicyType { get; set; }

        public int PatchingPolicyIDWR { get; set; }
        public int PatchingPolicyTypeIDWR { get; set; }
        [Required(ErrorMessage = "Please Enter Policy Name")]
        public string PolicyNameWR { get; set; }   
        public string WindowStartTimeWR { get; set; }
        public string WindowEndTimeWR { get; set; }
        public TimeZoneFX WindowTimeZoneWR { get; set; }
        public ScheduleType DefineScheduleTypeWR { get; set; }              
        public WeekOfDay WeekOfDayWR { get; set; }
        public MonthOfDay MonthOfDayWR { get; set; }
        public OptionType RebootPermissionWR { get; set; }
        public string RebootWindowStartTimeWR { get; set; }
        public string RebootWindowEndTimeWR { get; set; }
        public TimeZoneFX RebootWindowTimeZoneWR { get; set; }

        public int PatchingPolicyIDSR { get; set; }
        public int PatchingPolicyTypeIDSR { get; set; }
        [Required(ErrorMessage = "Please Enter Policy Name")]
        public string PolicyNameSR { get; set; }
        public string WindowStartTimeSR { get; set; }
        public string WindowEndTimeSR { get; set; }
        public TimeZoneFX WindowTimeZoneSR { get; set; }
        public ScheduleType DefineScheduleTypeSR { get; set; }
        public WeekOfDay WeekOfDaySR { get; set; }
        public MonthOfDay MonthOfDaySR { get; set; }
        public OptionType RebootPermissionSR { get; set; }
        public string RebootWindowStartTimeSR { get; set; }
        public string RebootWindowEndTimeSR { get; set; }
        public TimeZoneFX RebootWindowTimeZoneSR { get; set; }

        public int PatchingPolicyIDDND { get; set; }
        public int PatchingPolicyTypeIDND { get; set; }
        [Required(ErrorMessage = "Please Enter Policy Name")]
        public string PolicyNameND { get; set; }
        public string WindowStartTimeND { get; set; }
        public string WindowEndTimeND { get; set; }
        public TimeZoneFX WindowTimeZoneND { get; set; }
        public ScheduleType DefineScheduleTypeND { get; set; }
        public WeekOfDay WeekOfDayND { get; set; }
        public MonthOfDay MonthOfDayND { get; set; }
        public OptionType RebootPermissionND { get; set; }
        public string RebootWindowStartTimeND { get; set; }
        public string RebootWindowEndTimeND { get; set; }
        public TimeZoneFX RebootWindowTimeZoneND { get; set; }        
    }
}