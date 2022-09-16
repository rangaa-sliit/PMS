/*Developed By:- Dulanjalee Wickremasinghe
    Developed On:- 2022/08/25 */

using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.ViewModels
{
    public class SpecializationVM
    {
        public int SpecializationId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Nullable<int> DegreeId { get; set; }
        public string DegreeName { get; set; }
        public Nullable<int> InstituteId { get; set; }
        public string InstituteName { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public bool IsActive { get; set; }
    }
}