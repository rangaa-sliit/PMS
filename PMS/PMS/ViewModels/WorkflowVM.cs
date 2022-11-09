using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.ViewModels
{
    public class WorkflowVM
    {
        public int Id { get; set; }
        public string WorkflowName { get; set; }
        public string Description { get; set; }
        public string FacultyName { get; set; }
        public bool IsActive { get; set; }
    }
}