using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.Custom_Classes
{
    public class WorkflowCC
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Workflow Role is required")]
        public string WorkflowRole { get; set; }
        public string Prefix { get; set; }
        public string LandingRole { get; set; }
        public bool IsInitial { get; set; }
        public int CurrentPosition { get; set; }
        public bool IsActive { get; set; }
    }
}