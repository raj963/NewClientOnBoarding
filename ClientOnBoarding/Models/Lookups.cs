using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientOnBoarding.Models
{
    #region -- Lookup Classes --

    public class ScheduleType
    {
        public int? ID { get; set; }
        public string Name { get; set; }
    }

    public class WeekOfDay
    {
        public int? ID { get; set; }
        public string Name { get; set; }
    }

    public class WeekOfMonth
    {
        public int? ID { get; set; }
        public string Name { get; set; }
    }

    public class MonthOfDay
    {
        public int? ID { get; set; }
        public string Name { get; set; }
    }

    public class TimeZoneFX
    {
        public int? ID { get; set; }
        public string Name { get; set; }
    }

    public class PolicyType
    {
        public int? ID { get; set; }
        public string Name { get; set; }
    }

    public class DeviceType
    {
        public int? ID { get; set; }
        public string Name { get; set; }
    }

    public class ContactType
    {
        public int? ID { get; set; }
        public string Name { get; set; }
    }

    public class ScheduleTypeMaintenancePolicy
    {
        public int? ID { get; set; }
        public string Name { get; set; }
    }

    public class OptionType
    {
        public int? ID { get; set; }
        public string Name { get; set; }
    }

    public class SetUpStaus
    {
        public int? ID { get; set; }
        public string Name { get; set; }
    }

    public class FXTime
    {
        public int? ID { get; set; }
        public string Name { get; set; }
    }

    public class NOCCommunicationBy
    {
        public int? ID { get; set; }
        public string Name { get; set; }
    }

    public class ServiceType
    {
        public int? ID { get; set; }
        public string Name { get; set; }
    }

    #endregion -- Lookup Classes --

    #region -- Lookup Values (Binding) --

    public static class LookUpData
    {
        public static List<MonthOfDay> GetMonthOfDay()
        {
            List<MonthOfDay> Month = new List<MonthOfDay>();
            Month.Add(new MonthOfDay() { ID = 1, Name = "January" });
            Month.Add(new MonthOfDay() { ID = 2, Name = "February" });
            Month.Add(new MonthOfDay() { ID = 3, Name = "March" });
            Month.Add(new MonthOfDay() { ID = 4, Name = "April" });
            Month.Add(new MonthOfDay() { ID = 5, Name = "May" });
            Month.Add(new MonthOfDay() { ID = 6, Name = "June" });
            Month.Add(new MonthOfDay() { ID = 7, Name = "July" });
            Month.Add(new MonthOfDay() { ID = 8, Name = "August" });
            Month.Add(new MonthOfDay() { ID = 9, Name = "September" });
            Month.Add(new MonthOfDay() { ID = 10, Name = "October" });
            Month.Add(new MonthOfDay() { ID = 11, Name = "November" });
            Month.Add(new MonthOfDay() { ID = 12, Name = "December" });
            return Month;
        }

        public static List<ContactType> GetContactType()
        {
            List<ContactType> contacttype = new List<ContactType>();
            contacttype.Add(new ContactType() { ID = 1, Name = "Primary" });
            contacttype.Add(new ContactType() { ID = 2, Name = "BackUp" });
            return contacttype;
        }

        public static List<DeviceType> GetDeviceType()
        {
            List<DeviceType> devicetype = new List<DeviceType>();
            devicetype.Add(new DeviceType() { ID = 1, Name = "Server" });
            devicetype.Add(new DeviceType() { ID = 2, Name = "Desktop" });
            devicetype.Add(new DeviceType() { ID = 3, Name = "Router" });
            devicetype.Add(new DeviceType() { ID = 4, Name = "Switch" });
            devicetype.Add(new DeviceType() { ID = 5, Name = "Firewall" });
            devicetype.Add(new DeviceType() { ID = 6, Name = "Printer" });
            devicetype.Add(new DeviceType() { ID = 7, Name = "Misc" });
            return devicetype;
        }

        public static List<OptionType> GetOptionType()
        {
            List<OptionType> lstOptionType = new List<OptionType>();
            lstOptionType.Add(new OptionType() { ID = 1, Name = "Yes" });
            lstOptionType.Add(new OptionType() { ID = 2, Name = "No" });
            return lstOptionType;
        }

        public static List<SetUpStaus> GetSetupUpStatus()
        {
            List<SetUpStaus> setupstatus = new List<SetUpStaus>();
            setupstatus.Add(new SetUpStaus() { ID = 1, Name = "SetUp Pending" });
            setupstatus.Add(new SetUpStaus() { ID = 2, Name = "Under SetUp" });
            setupstatus.Add(new SetUpStaus() { ID = 3, Name = "SetUp Complete" });
            return setupstatus;
        }

        public static List<TimeZoneFX> GetTimeZone()
        {
            List<TimeZoneFX> TimeZone = new List<TimeZoneFX>();
            TimeZone.Add(new TimeZoneFX() { ID = 1, Name = "Eastern" });
            TimeZone.Add(new TimeZoneFX() { ID = 2, Name = "Central" });
            TimeZone.Add(new TimeZoneFX() { ID = 3, Name = "Mountain" });
            TimeZone.Add(new TimeZoneFX() { ID = 4, Name = "Pacific" });
            return TimeZone;
        }
        public static List<TimeZoneFX> GetTimeZoneCI()
        {
            List<TimeZoneFX> TimeZone = new List<TimeZoneFX>();
            TimeZone.Add(new TimeZoneFX() { ID = 1, Name = "Eastern" });
            TimeZone.Add(new TimeZoneFX() { ID = 2, Name = "Central" });
            TimeZone.Add(new TimeZoneFX() { ID = 3, Name = "Mountain" });
            TimeZone.Add(new TimeZoneFX() { ID = 4, Name = "Pacific" });
            TimeZone.Add(new TimeZoneFX() { ID = 5, Name = "European" });
            return TimeZone;
        }

        public static List<NOCCommunicationBy> GetNOCCommunicationBy()
        {
            List<NOCCommunicationBy> NOCCommunicationBy = new List<NOCCommunicationBy>();
            NOCCommunicationBy.Add(new NOCCommunicationBy() { ID = 1, Name = "Directly" });
            NOCCommunicationBy.Add(new NOCCommunicationBy() { ID = 2, Name = "Through Customer Contact" });
            return NOCCommunicationBy;
        }

        public static List<PolicyType> GetPolicyType()
        {
            List<PolicyType> lstPolicyType = new List<PolicyType>();
            lstPolicyType.Add(new PolicyType() { ID = 1, Name = "All" });
            lstPolicyType.Add(new PolicyType() { ID = 2, Name = "All Active Customer And Prospect" });
            lstPolicyType.Add(new PolicyType() { ID = 3, Name = "Active Customer Only" });
            lstPolicyType.Add(new PolicyType() { ID = 4, Name = "Active Prospect Only" });
            lstPolicyType.Add(new PolicyType() { ID = 5, Name = "All In Active Customer And Prospect" });
            lstPolicyType.Add(new PolicyType() { ID = 6, Name = "In Active Customer Only" });
            lstPolicyType.Add(new PolicyType() { ID = 7, Name = "In Active Prospect Only" });
            return lstPolicyType;
        }

        public static List<FXTime> GetFXTime()
        {
            List<FXTime> time = new List<FXTime>();
            time.Add(new FXTime() { ID = 1, Name = "1:00" });
            time.Add(new FXTime() { ID = 2, Name = "2:00" });
            time.Add(new FXTime() { ID = 3, Name = "3:00" });
            time.Add(new FXTime() { ID = 4, Name = "4:00" });
            time.Add(new FXTime() { ID = 5, Name = "5:00" });
            time.Add(new FXTime() { ID = 6, Name = "6:00" });
            time.Add(new FXTime() { ID = 7, Name = "7:00" });
            time.Add(new FXTime() { ID = 8, Name = "8:00" });
            time.Add(new FXTime() { ID = 9, Name = "9:00" });
            time.Add(new FXTime() { ID = 10, Name = "10:00" });
            time.Add(new FXTime() { ID = 11, Name = "11:00" });
            time.Add(new FXTime() { ID = 12, Name = "12:00" });
            time.Add(new FXTime() { ID = 13, Name = "13:00" });
            time.Add(new FXTime() { ID = 14, Name = "14:00" });
            time.Add(new FXTime() { ID = 15, Name = "15:00" });
            time.Add(new FXTime() { ID = 16, Name = "16:00" });
            time.Add(new FXTime() { ID = 17, Name = "17:00" });
            time.Add(new FXTime() { ID = 18, Name = "18:00" });
            time.Add(new FXTime() { ID = 19, Name = "19:00" });
            time.Add(new FXTime() { ID = 20, Name = "20:00" });
            time.Add(new FXTime() { ID = 21, Name = "21:00" });
            time.Add(new FXTime() { ID = 22, Name = "22:00" });
            time.Add(new FXTime() { ID = 23, Name = "23:00" });
            time.Add(new FXTime() { ID = 24, Name = "0:00" });
            return time;
        }

        public static List<WeekOfDay> GetWeekOfDay()
        {
            List<WeekOfDay> Week = new List<WeekOfDay>();
            Week.Add(new WeekOfDay() { ID = 1, Name = "Sunday" });
            Week.Add(new WeekOfDay() { ID = 2, Name = "Monday" });
            Week.Add(new WeekOfDay() { ID = 3, Name = "Tuesday" });
            Week.Add(new WeekOfDay() { ID = 4, Name = "Wednesday" });
            Week.Add(new WeekOfDay() { ID = 5, Name = "Thursday" });
            Week.Add(new WeekOfDay() { ID = 6, Name = "Friday" });
            Week.Add(new WeekOfDay() { ID = 7, Name = "Saturday" });
            return Week;
        }

        public static List<WeekOfMonth> GetWeekOfMonth()
        {
            List<WeekOfMonth> WeekOfMonth = new List<WeekOfMonth>();
            WeekOfMonth.Add(new WeekOfMonth() { ID = 1, Name = "Week1" });
            WeekOfMonth.Add(new WeekOfMonth() { ID = 2, Name = "Week2" });
            WeekOfMonth.Add(new WeekOfMonth() { ID = 3, Name = "Week3" });
            WeekOfMonth.Add(new WeekOfMonth() { ID = 4, Name = "Week4" });
            return WeekOfMonth;
        }

        public static List<ScheduleType> GetScheduleType()
        {
            List<ScheduleType> ScheduleType = new List<ScheduleType>();
            ScheduleType.Add(new ScheduleType() { ID = 1, Name = "Daily" });
            ScheduleType.Add(new ScheduleType() { ID = 2, Name = "Weekly" });
            ScheduleType.Add(new ScheduleType() { ID = 3, Name = "Monthly" });
            return ScheduleType;
        }

        public static List<SetUpStaus> GetSetUpStatus()
        {
            List<SetUpStaus> setupstatus = new List<SetUpStaus>();
            setupstatus.Add(new SetUpStaus() { ID = 1, Name = "SetUp Pending" });
            setupstatus.Add(new SetUpStaus() { ID = 2, Name = "Under SetUp" });
            setupstatus.Add(new SetUpStaus() { ID = 3, Name = "SetUp Complete" });
            return setupstatus;
        }

        public static List<ServiceType> GetServiceType()
        {
            List<ServiceType> lstServiceType = new List<ServiceType>();
            lstServiceType.Add(new ServiceType() { ID = 1, Name = "MANAGE" });
            lstServiceType.Add(new ServiceType() { ID = 2, Name = "RESPOND" });
            return lstServiceType;
        }
    }

    #endregion -- Lookup Values (Binding) --

    #region -- Get Lookup Value --

    public static class LookUpValue
    {
        public static string GetAccessPolicyType(int ID)
        {
            string Name = string.Empty;
            switch (ID)
            {
                case 1:
                    Name = "Workstation";
                    break;

                case 2:
                    Name = "Server";
                    break;

                case 3:
                    Name = "Network Device";
                    break;

            }
            return Name;
        }

        public static string GetScheduleType(int ID)
        {
            string Name = string.Empty;
            switch (ID)
            {
                case 1:
                    Name = "Daily";
                    break;

                case 2:
                    Name = "Weekly";
                    break;

                case 3:
                    Name = "Monthly";
                    break;

            }
            return Name;
        }

        public static string GetTimeZone(int ID)
        {
            string Name = string.Empty;
            switch (ID)
            {
                case 1:
                    Name = "Eastern";
                    break;

                case 2:
                    Name = "Central";
                    break;

                case 3:
                    Name = "Mountain";
                    break;
                case 4:
                    Name = "Pacific";
                    break;
                case 5:
                    Name = "European";
                    break;
            }
            return Name;
        }

     

        public static string GetContactType(int ID)
        {
            string Name = string.Empty;
            switch (ID)
            {
                case 1:
                    Name = "Primary";
                    break;

                case 2:
                    Name = "Back Up";
                    break;

            }
            return Name;
        }

        public static string GetWeekOfDay(int ID)
        {
            string Name = string.Empty;
            switch (ID)
            {
                case 1: ;
                    Name = "Sunday";
                    break;

                case 2:
                    Name = "Monday";
                    break;

                case 3:
                    Name = "Tuesday";
                    break;

                case 4:
                    Name = "Wednesday";
                    break;

                case 5:
                    Name = "Thursday";
                    break;

                case 6:
                    Name = "Friday";
                    break;

                case 7:
                    Name = "Saturday";
                    break;

            }
            return Name;
        }

        public static string GetMonthOfDays(int ID)
        {
            string Name = string.Empty;
            switch (ID)
            {
                case 1: ;
                    Name = "January";
                    break;

                case 2:
                    Name = "February";
                    break;

                case 3:
                    Name = "March";
                    break;

                case 4:
                    Name = "April";
                    break;

                case 5:
                    Name = "May";
                    break;

                case 6:
                    Name = "June";
                    break;

                case 7:
                    Name = "July";
                    break;
                case 8:
                    Name = "August" ;
                    break;
                case 9:
                    Name = "September";
                    break;
                case 10:
                    Name = "October";
                    break;
                case 11:
                    Name = "November";
                    break;
                case 12:
                    Name = "December";
                    break;

            }
            return Name;
        }

        public static string GetFXTime(int ID)
        {
            string Name = string.Empty;
            switch (ID)
            {
                case 1: ;
                    Name = "1:00";
                    break;

                case 2:
                    Name = "2:00";
                    break;

                case 3:
                    Name = "3:00";
                    break;

                case 4:
                    Name = "4:00";
                    break;

                case 5:
                    Name = "5:00";
                    break;

                case 6:
                    Name = "6:00";
                    break;

                case 7:
                    Name = "7:00";
                    break;
                case 8:
                    Name = "8:00";
                    break;
                case 9:
                    Name = "9:00";
                    break;
                case 10:
                    Name = "10:00";
                    break;
                case 11:
                    Name = "11:00";
                    break;
                case 12:
                    Name = "12:00";
                    break;
                case 13: ;
                    Name = "13:00";
                    break;

                case 14:
                    Name = "14:00";
                    break;

                case 15:
                    Name = "15:00";
                    break;

                case 16:
                    Name = "16:00";
                    break;

                case 17:
                    Name = "17:00";
                    break;

                case 18:
                    Name = "18:00";
                    break;

                case 19:
                    Name = "19:00";
                    break;
                case 20:
                    Name = "20:00";
                    break;
                case 21:
                    Name = "21:00";
                    break;
                case 22:
                    Name = "22:00";
                    break;
                case 23:
                    Name = "23:00";
                    break;
                case 24:
                    Name = "24:00";
                    break;
            }
            return Name;
        }

        public static string GetSetUpStaus(int ID)
        {
            string Name = string.Empty;
            switch (ID)
            {
                case 1:
                    Name = "SetUp Pending";
                    break;
                case 2:
                    Name = "Under SetUp";
                    break;
                case 3:
                    Name = "SetUp Complete";
                    break;
            }
            return Name;
        }

        public static string GetDeviceType(int ID)
        {
            string Name = string.Empty;
            switch (ID)
            {
                case 1: ;
                    Name = "Server";
                    break;

                case 2:
                    Name = "Desktop";
                    break;

                case 3:
                    Name = "Router";
                    break;

                case 4:
                    Name = "Switch";
                    break;

                case 5:
                    Name = "Firewall";
                    break;

                case 6:
                    Name = "Printer";
                    break;

                case 7:
                    Name = "Misc";
                    break;
            }
            return Name;
        }

        public static string GetOptionType(int ID)
        {
            string Name = string.Empty;
            switch (ID)
            {
                case 1:
                    Name = "Yes";
                    break;
                case 2:
                    Name = "No";
                    break;
            }
            return Name;
        }

        public static string GetServiceType(int ID)
        {
            string Name = string.Empty;
            switch (ID)
            {
                case 1:
                    Name = "MANAGE";
                    break;
                case 2:
                    Name = "RESPOND";
                    break;
            }
            return Name;
        }

        public static string GetNOCCommunicationBy(int ID)
        {            
            string Name = string.Empty;
            switch (ID)
            {
                case 1:
                    Name = "Through MSP";
                    break;
                case 2:
                    Name = "VAR Contact";
                    break;
            }
            return Name;
        }
    }

    #endregion -- Get Lookup Value --
}