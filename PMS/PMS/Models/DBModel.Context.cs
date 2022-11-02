﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PMSEntities : DbContext
    {
        public PMSEntities()
            : base("name=PMSEntities")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AccessGroup> AccessGroup { get; set; }
        public virtual DbSet<AccessGroupClaims> AccessGroupClaims { get; set; }
        public virtual DbSet<Appointment> Appointment { get; set; }
        public virtual DbSet<AppointmentType> AppointmentType { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<CalendarPeriod> CalendarPeriod { get; set; }
        public virtual DbSet<Campus> Campus { get; set; }
        public virtual DbSet<Claim> Claim { get; set; }
        public virtual DbSet<ConductedLectures> ConductedLectures { get; set; }
        public virtual DbSet<ConductedLecturesLog> ConductedLecturesLog { get; set; }
        public virtual DbSet<ConfigurationalSettings> ConfigurationalSettings { get; set; }
        public virtual DbSet<Degree> Degree { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Designation> Designation { get; set; }
        public virtual DbSet<Faculty> Faculty { get; set; }
        public virtual DbSet<Institute> Institute { get; set; }
        public virtual DbSet<Intake> Intake { get; set; }
        public virtual DbSet<LectureHall> LectureHall { get; set; }
        public virtual DbSet<LecturerAssignments> LecturerAssignments { get; set; }
        public virtual DbSet<LectureTimetable> LectureTimetable { get; set; }
        public virtual DbSet<LectureTimetableLog> LectureTimetableLog { get; set; }
        public virtual DbSet<LectureType> LectureType { get; set; }
        public virtual DbSet<PaymentRate> PaymentRate { get; set; }
        public virtual DbSet<PaymentRateLog> PaymentRateLog { get; set; }
        public virtual DbSet<SemesterRegistration> SemesterRegistration { get; set; }
        public virtual DbSet<SemesterSubject> SemesterSubject { get; set; }
        public virtual DbSet<Specialization> Specialization { get; set; }
        public virtual DbSet<StudentBatch> StudentBatch { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<SubWorkflows> SubWorkflows { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Title> Title { get; set; }
        public virtual DbSet<Workflows> Workflows { get; set; }
    }
}
