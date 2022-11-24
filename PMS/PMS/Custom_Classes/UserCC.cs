using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.Custom_Classes
{
    public class UserCC
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Employee Number is required")]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters exceeded")]
        public string EmployeeNumber { get; set; }
        [Required(ErrorMessage = "Employee Title is required")]
        public int EmployeeTitle { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(150, ErrorMessage = "Maximum 150 characters exceeded")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        [MaxLength(150, ErrorMessage = "Maximum 150 characters exceeded")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Employee Email is required")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(150, ErrorMessage = "Maximum 150 characters exceeded")]
        public string Email { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum 50 characters exceeded")]
        public string PhoneNumber { get; set; }
    }
}