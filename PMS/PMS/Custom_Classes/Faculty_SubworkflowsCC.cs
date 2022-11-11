using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Custom_Classes
{
    public class Faculty_SubworkflowsCC
    {
        public int FacultyId { get; set; }
        public List<SubWorkflow_WorkflowCC> SubworkflowList { get; set; }
    }
}