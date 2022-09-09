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

    public partial class Intake
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Intake()
        {
            this.SemesterRegistration = new HashSet<SemesterRegistration>();
        }
    
        public int IntakeId { get; set; }
        [Required(ErrorMessage = "From Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FromDate { get; set; }
        [Required(ErrorMessage = "To Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ToDate { get; set; }
        [MaxLength(150, ErrorMessage = "Maximum 150 characters exceeded")]
        public string IntakeCode { get; set; }
        [Required(ErrorMessage = "Intake Name is required")]
        [MaxLength(200, ErrorMessage = "Maximum 200 characters exceeded")]
        public string IntakeName { get; set; }
        [Required(ErrorMessage = "Intake Year is required")]
        [Range(2015, int.MaxValue, ErrorMessage = "Years grater than 2015 are allowed")]
        public Nullable<int> IntakeYear { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SemesterRegistration> SemesterRegistration { get; set; }
    }
}
