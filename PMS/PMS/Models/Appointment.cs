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
    
    public partial class Appointment
    {
        public int AppointmentId { get; set; }
        public string UserId { get; set; }
        public int AppointmentTypeId { get; set; }
        public System.DateTime AppointmentFrom { get; set; }
        public Nullable<System.DateTime> AppointmentTo { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    
        public virtual AppointmentType AppointmentType { get; set; }
        public virtual AspNetUsers AspNetUsers { get; set; }
    }
}
