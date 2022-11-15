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
        [Required(ErrorMessage = "Employment Type is required")]
        public int AppointmentTypeId { get; set; }
        [Required(ErrorMessage = "Department is required")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Faculty is required")]
        public int FacultyId { get; set; }
        [Required(ErrorMessage = "Degree is required")]
        public int DegreeId { get; set; }
        [Required(ErrorMessage = "Subject is required")]
        public int SubjectId { get; set; }
        [Required(ErrorMessage = "Campus is required")]
        public int CampusId { get; set; }
        [Required(ErrorMessage = "Start Date is required")]
        public string StartDate { get; set; }
        [Required(ErrorMessage = "End Date is required")]
        public string EndDate { get; set; }
        public string SelectedTable { get; set; }
    }
}