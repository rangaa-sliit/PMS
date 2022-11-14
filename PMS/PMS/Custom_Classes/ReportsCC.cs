using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.Custom_Classes
{
    public class ReportsCC
    {
        [Required(ErrorMessage = "Employee is required")]
        public string LecturerId { get; set; }
        [Required(ErrorMessage = "Start Date is required")]
        public string StartDate { get; set; }
        [Required(ErrorMessage = "End Date is required")]
        public string EndDate { get; set; }
    }
}