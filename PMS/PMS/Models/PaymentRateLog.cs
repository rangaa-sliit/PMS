//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PaymentRateLog
    {
        public int Id { get; set; }
        public Nullable<int> DegreeId { get; set; }
        public Nullable<int> SpecializationId { get; set; }
        public Nullable<int> FacultyId { get; set; }
        public Nullable<int> SubjectId { get; set; }
        public int DesignationId { get; set; }
        public double RatePerHour { get; set; }
        public bool IsApproved { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public int PaymentRateId { get; set; }
    
        public virtual Degree Degree { get; set; }
        public virtual Designation Designation { get; set; }
        public virtual Faculty Faculty { get; set; }
        public virtual PaymentRate PaymentRate { get; set; }
        public virtual Specialization Specialization { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
