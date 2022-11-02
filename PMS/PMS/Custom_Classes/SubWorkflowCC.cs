using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.Custom_Classes
{
    public class SubWorkflowCC
    {
        public int SubWorkflowId { get; set; }
        public int WorkflowId { get; set; }
        [Required(ErrorMessage = "Workflow Role is required")]
        public string WorkflowRole { get; set; }
        public string Prefix { get; set; }
        public string LandingRole { get; set; }
        public bool IsInitial { get; set; }
        public int CurrentPosition { get; set; }
        public bool IsSpecificUser { get; set; }
        public string WorkflowUser { get; set; }
        public bool IsActive { get; set; }
    }
}