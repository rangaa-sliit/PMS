using PMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.ViewModels
{
    public class DepartmentVM
    {
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Department Code is required")]
        [MaxLength(100, ErrorMessage = "Maximum 100 characters exceeded")]
        public string DepartmentCode { get; set; }
        [Required(ErrorMessage = "Department Name is required")]
        [MaxLength(500, ErrorMessage = "Maximum 500 characters exceeded")]
        public string DepartmentName { get; set; }
        public string HODId { get; set; }
        public AspNetUsers HOD { get; set; }
        public Nullable<int> FacultyId { get; set; }
        public Faculty Faculty { get; set; }
        public bool IsActive { get; set; }
    }
}