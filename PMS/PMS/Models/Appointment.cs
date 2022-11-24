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
    using System.ComponentModel.DataAnnotations;

    public partial class Appointment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Appointment()
        {
            this.AppointmentLog = new HashSet<AppointmentLog>();
        }
    
        public int AppointmentId { get; set; }
        [Required(ErrorMessage = "Employee is required")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Appointment Type is required")]
        public int AppointmentTypeId { get; set; }
        [Required(ErrorMessage = "Designation is required")]
        public int DesignationId { get; set; }
        [Required(ErrorMessage = "Appointment From Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> AppointmentFrom { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> AppointmentTo { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string Comment { get; set; }
        public string AutoUpdateRemark { get; set; }
        public Nullable<System.DateTime> AutoUpdateDate { get; set; }
        public bool IsActive { get; set; }
    
        public virtual AppointmentType AppointmentType { get; set; }
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual Designation Designation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AppointmentLog> AppointmentLog { get; set; }
    }
}
