//Developed By:- Ranga Athapaththu
//Developed On:- 2022/08/19

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
        public string FacultyCode { get; set; }
        public string FacultyName { get; set; }
        public string FacultyDeanId { get; set; }
        public string FacultyDean { get; set; }
        public bool IsActive { get; set; }
    }
}