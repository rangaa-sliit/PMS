/*Developed By:- Dulanjalee Wickremasinghe
    Developed On:- 2022/08/24 */

using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.ViewModels
{
    public class DegreeVM
    {
        public int DegreeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> FacultyId { get; set; }
        public string FacultyName { get; set; }
        public Nullable<int> InstituteId { get; set; }
        public string InstituteName { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public bool IsActive { get; set; }
    }
}