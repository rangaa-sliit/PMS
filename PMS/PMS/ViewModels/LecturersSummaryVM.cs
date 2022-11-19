using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.ViewModels
{
    public class LecturersSummaryVM
    {
        public string LecturerEmpNumber { get; set; }
        public string LecturerName { get; set; }
        public string LecturerEmail { get; set; }
        public string LecturerContactNumber { get; set; }
        public string FacultyName { get; set; }
        public bool IsActive { get; set; }
        public string LecturerDesignation { get; set; }
    }
}