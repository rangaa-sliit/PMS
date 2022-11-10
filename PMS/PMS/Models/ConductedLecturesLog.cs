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
    
    public partial class ConductedLecturesLog
    {
        public int CLLogId { get; set; }
        public int CLId { get; set; }
        public int TimetableId { get; set; }
        public System.DateTime ActualLectureDate { get; set; }
        public System.TimeSpan ActualFromTime { get; set; }
        public System.TimeSpan ActualToTime { get; set; }
        public Nullable<int> ActualLocationId { get; set; }
        public Nullable<int> CampusId { get; set; }
        public string StudentBatches { get; set; }
        public Nullable<int> StudentCount { get; set; }
        public string StudentAttendanceSheetLocation { get; set; }
        public string Comment { get; set; }
        public Nullable<int> CurrentStage { get; set; }
        public string CurrentStageDisplayName { get; set; }
        public Nullable<bool> IsApprovedOrRejected { get; set; }
        public Nullable<System.DateTime> ApprovedOrRejectedDate { get; set; }
        public string ApprovedOrRejectedBy { get; set; }
        public string ApprovedOrRejectedRemark { get; set; }
        public bool IsOpenForModerations { get; set; }
        public Nullable<System.DateTime> ModerationOpenedDate { get; set; }
        public string ModerationOpenedBy { get; set; }
        public string ModerationOpenedRemark { get; set; }
        public Nullable<double> PaymentAmount { get; set; }
        public bool IsFinalApproved { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    
        public virtual Campus Campus { get; set; }
        public virtual ConductedLectures ConductedLectures { get; set; }
        public virtual LectureHall LectureHall { get; set; }
        public virtual LectureTimetable LectureTimetable { get; set; }
    }
}
