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

    public partial class AspNetUsers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AspNetUsers()
        {
            this.Appointment = new HashSet<Appointment>();
            this.AspNetUserClaims = new HashSet<AspNetUserClaims>();
            this.AspNetUserLogins = new HashSet<AspNetUserLogins>();
            this.AspNetUserRoles = new HashSet<AspNetUserRoles>();
            this.Department = new HashSet<Department>();
            this.Faculty1 = new HashSet<Faculty>();
        }
    
        public string Id { get; set; }
        [Required(ErrorMessage = "Employee Number is required")]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters exceeded")]
        public string EmployeeNumber { get; set; }
        [Required(ErrorMessage = "Employee Title is required")]
        public int EmployeeTitle { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(150, ErrorMessage = "Maximum 150 characters exceeded")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        [MaxLength(150, ErrorMessage = "Maximum 150 characters exceeded")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Employee Email is required")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(150, ErrorMessage = "Maximum 150 characters exceeded")]
        public string Email { get; set; }
        public string UserName { get; set; }
        [MaxLength(50, ErrorMessage = "Maximum 50 characters exceeded")]
        public string PhoneNumber { get; set; }
        public Nullable<int> FacultyId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> AccessFailedCount { get; set; }
        public string ConcurrencyStamp { get; set; }
        public Nullable<bool> EmailConfirmed { get; set; }
        public Nullable<bool> LockoutEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public string NormalizedEmail { get; set; }
        public string NormalizedUserName { get; set; }
        public string PasswordHash { get; set; }
        public Nullable<bool> PhoneNumberConfirmed { get; set; }
        public string SecurityStamp { get; set; }
        public Nullable<bool> TwoFactorEnabled { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appointment> Appointment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual Faculty Faculty { get; set; }
        public virtual Title Title { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Department> Department { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Faculty> Faculty1 { get; set; }
    }
}
