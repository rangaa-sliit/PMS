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

    public partial class SemesterRegistration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SemesterRegistration()
        {
            this.LectureTimetable = new HashSet<LectureTimetable>();
            this.LectureTimetableLog = new HashSet<LectureTimetableLog>();
            this.SemesterSubject = new HashSet<SemesterSubject>();
            this.StudentBatch = new HashSet<StudentBatch>();
        }
    
        public int SemesterId { get; set; }
        [Required(ErrorMessage = "Calendar Year is required")]
        [Range(2015, int.MaxValue, ErrorMessage = "Years grater than 2015 are allowed")]
        public Nullable<int> CalendarYear { get; set; }
        [Required(ErrorMessage = "Calendar Period is required")]
        public Nullable<int> CalendarPeriodId { get; set; }
        [Required(ErrorMessage = "Intake Year is required")]
        public Nullable<int> IntakeYear { get; set; }
        [Required(ErrorMessage = "Intake is required")]
        public Nullable<int> IntakeId { get; set; }
        [Required(ErrorMessage = "Academic Year is required")]
        public Nullable<int> AcademicYear { get; set; }
        [Required(ErrorMessage = "Academic Semester is required")]
        public Nullable<int> AcademicSemester { get; set; }
        [Required(ErrorMessage = "Faculty is required")]
        public Nullable<int> FacultyId { get; set; }
        [Required(ErrorMessage = "Institute is required")]
        public Nullable<int> InstituteId { get; set; }
        [Required(ErrorMessage = "Degree is required")]
        public Nullable<int> DegreeId { get; set; }
        public Nullable<int> SpecializationId { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        [Required(ErrorMessage = "Semester Starting Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> FromDate { get; set; }
        [Required(ErrorMessage = "Semester End Date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ToDate { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    
        public virtual CalendarPeriod CalendarPeriod { get; set; }
        public virtual Degree Degree { get; set; }
        public virtual Department Department { get; set; }
        public virtual Faculty Faculty { get; set; }
        public virtual Institute Institute { get; set; }
        public virtual Intake Intake { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LectureTimetable> LectureTimetable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LectureTimetableLog> LectureTimetableLog { get; set; }
        public virtual Specialization Specialization { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SemesterSubject> SemesterSubject { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StudentBatch> StudentBatch { get; set; }
    }
}
