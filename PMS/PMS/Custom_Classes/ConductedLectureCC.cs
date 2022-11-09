using PMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Custom_Classes
{
    public class ConductedLectureCC
    {
        public System.DateTime ActualLectureDate { get; set; }
        public System.TimeSpan ActualFromTime { get; set; }
        public System.TimeSpan ActualToTime { get; set; }
        public Nullable<int> ActualLocationId { get; set; }
        public Nullable<int> CampusId { get; set; }
        public string StudentAttendanceSheetLocation { get; set; }
        public string Comment { get; set; }
        public int CurrentStageId { get; set; }
        public Nullable<bool> IsApprovedOrRejected { get; set; }
        public Nullable<System.DateTime> ApprovedOrRejectedDate { get; set; }
        public string ApprovedOrRejectedBy { get; set; }
        public string ApprovedOrRejectedRemark { get; set; }
        public bool IsOpenForModerations { get; set; }
        public bool IsActive { get; set; }

        public SemesterTimetableVM SemesterTimetableRecords { get; set; }
    }
}