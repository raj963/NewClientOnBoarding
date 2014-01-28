using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClientOnBoarding.Models
{
    public class tbltoolInfomation
    {
        public int ToolType { get; set; }
        public int CustomerID { get; set; }

        public int ToolTypeRT { get; set; }
        public int ToolIDRT { get; set; }
        [Required(ErrorMessage = "Please Enter RMM Tool")]       
        public String RMMTool { get; set; }
      
        public String RMMUrl { get; set; }
        public String RMMToolUserName { get; set; }
        public String RMMToolPassword { get; set; }

        public int ToolTypePT { get; set; }
        public int ToolIDPT { get; set; }
        [Required(ErrorMessage = "Please Enter PSA Tool")]
        public String PSATool { get; set; }
  
        public String PSAUrl { get; set; }
        public String PSAToolUserName { get; set; }
        public String PSAToolPassword { get; set; }
    }
}