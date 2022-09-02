/*Developed By:- Dulanjalee Wickremasinghe
    Developed On:- 2022/08/31 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.ViewModels
{
    public class PaymentRateVM
    {

        public int Id { get; set; }
        public int RatePerHour { get; set; }
        public string DesignationName { get; set; }
        public Nullable<int> DesignationId { get; set; }
        public string FacultyName { get; set; }
        public Nullable<int> FacultyId { get; set; }
        public string DegreeName { get; set; }
        public Nullable<int> DegreeId { get; set; }
        public string SpecializationName { get; set; }
        public Nullable<int> SpecializationId { get; set; }
        public string SubjectName { get; set; }
        public Nullable<int> SubjectId { get; set; }
        public bool IsActive { get; set; }
    }
}