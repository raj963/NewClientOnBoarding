using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientOnBoarding.Models
{
    public class tblAccessPolicy
    {
        public int CustomerID { get; set; }
        public PolicyType AccessPolicyType { get; set; }
        public ScheduleTypeMaintenancePolicy ScheduleType { get; set; }

        public string productname { get; set; }
        public int AccessPolicyID { get; set; }
        public int AccessPolicyIDWR { get; set; }
        public OptionType RemoteControlPermissionWR { get; set; }
        public string RemoteAccessStartTimeWR { get; set; }
        public string RemoteAccessEndTimeWR { get; set; }
        public TimeZoneFX RemoteAccessTimeZoneWR { get; set; }
        public OptionType RebootPermissionWR { get; set; }
        public string RebootWindowStartTimeWR { get; set; }
        public string RebootWindowEndTimeWR { get; set; }
        public TimeZoneFX RebootTimeZoneWR { get; set; }

        public int AccessPolicyIDSR { get; set; }
        public OptionType RemoteControlPermissionSR { get; set; }
        public string RemoteAccessStartTimeSR { get; set; }
        public string RemoteAccessEndTimeSR { get; set; }
        public TimeZoneFX RemoteAccessTimeZoneSR { get; set; }
        public OptionType RebootPermissionSR { get; set; }
        public string RebootWindowStartTimeSR { get; set; }
        public string RebootWindowEndTimeSR { get; set; }
        public TimeZoneFX RebootTimeZoneSR { get; set; }

        public int AccessPolicyIDND { get; set; }
        public OptionType RemoteControlPermissionND { get; set; }
        public string RemoteAccessStartTimeND { get; set; }
        public string RemoteAccessEndTimeND { get; set; }
        public TimeZoneFX RemoteAccessTimeZoneND { get; set; }
        public OptionType RebootPermissionND { get; set; }
        public string RebootWindowStartTimeND { get; set; }
        public string RebootWindowEndTimeND { get; set; }
        public TimeZoneFX RebootTimeZoneND { get; set; }
    }
}