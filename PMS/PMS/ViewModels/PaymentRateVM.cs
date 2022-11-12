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
        public double CurrentRatePerHour { get; set; }
        public double OldRatePerHour { get; set; }
        public string DesignationName { get; set; }
        public string FacultyName { get; set; }
        public string DegreeName { get; set; }
        public string SpecializationName { get; set; }
        public string SubjectName { get; set; }
        public Nullable<bool> SentForApproval { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public string ApprovalOrRejectionRemark { get; set; }
        public bool IsActive { get; set; }
    }
}