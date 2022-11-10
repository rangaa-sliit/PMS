using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Custom_Classes
{
    public class SubWorkflow_WorkflowCC
    {
        public SubWorkflows SubWorkflowRecord { get; set; }
        public string WorkflowRole { get; set; }
        public Workflows WorkflowRecord { get; set; }
    }
}