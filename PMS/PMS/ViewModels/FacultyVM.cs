using PMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.ViewModels
{
    public class FacultyVM
    {
        public int FacultyId { get; set; }
        [Required(ErrorMessage = "Faculty Code is required")]
        [MaxLength(100, ErrorMessage = "Maximum 100 characters exceeded")]
        public string FacultyCode { get; set; }
        [Required(ErrorMessage = "Faculty Name is required")]
        [MaxLength(200, ErrorMessage = "Maximum 200 characters exceeded")]
        public string FacultyName { get; set; }
        public string FacultyDeanId { get; set; }
        public AspNetUsers FacultyDean { get; set; }
        public bool IsActive { get; set; }
    }
}