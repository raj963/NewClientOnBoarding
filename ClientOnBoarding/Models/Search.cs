using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientOnBoarding.Models
{
    public class Search
    {
        
       public string Client { get; set; }
       public string Customer { get; set; }
       public string DeviceId { get; set; }
    }

    public class SearchResult
    {
        public int ClientSiteDeviceID { get; set; }
        public string CustomerName { get; set; }
        public string ClientSiteName { get; set; }
        public string ClientSiteStatus { get; set; }
        public string DeviceDesc{ get; set; }
        public string DeviceName { get; set; }
        public int AccessPolicyID { get; set; }
        public string AccessPolicy { get; set; }        
        public int MaintenancePolicyID { get; set; }
        public string MaintenancePolicy { get; set; }
        public int PatchingPolicyID { get; set; }
        public string PatchingPolicy { get; set; }
        public int AntiVirusPolicyID { get; set; }
        public string AntiVirusPolicy { get; set; }
        public int BackUpPolicyID { get; set; }
        public string BackUpPolicy { get; set; }
        public int RMMToolID { get; set; }
        public string RMMTool { get; set; }
    }
}