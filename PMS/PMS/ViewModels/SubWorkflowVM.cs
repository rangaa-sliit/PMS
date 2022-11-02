using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.ViewModels
{
    public class SubWorkflowVM
    {
        public int SubWorkflowId { get; set; }
        public int WorkflowId { get; set; }
        public string WorkflowRole { get; set; }
        public int WorkflowStep { get; set; }
        public bool IsSpecificUser { get; set; }
        public string WorkflowUser { get; set; }
        public bool IsActive { get; set; }
        public List<string> WorkflowMapRoles { get; set; }
    }
}