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
    public class DepartmentVM
    {
        public int DepartmentId { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public string HODId { get; set; }
        public string HODName { get; set; }
        public Nullable<int> FacultyId { get; set; }
        public string FacultyName { get; set; }
        public bool IsActive { get; set; }
    }
}