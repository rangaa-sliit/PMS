using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.ViewModels
{
    public class ConductedLecturesVM
    {
        public int CLId { get; set; }
        public int TimetableId { get; set; }
        public string ActualLectureDate { get; set; }
        public string ActualFromTime { get; set; }
        public string ActualToTime { get; set; }
        public string ActualLocation { get; set; }
        public string CampusName { get; set; }
        public string StudentBatches { get; set; }
        public Nullable<int> StudentCount { get; set; }
        public string StudentAttendanceSheetLocation { get; set; }
        public string Comment { get; set; }
        public int CurrentStage { get; set; }
        public string CurrentStageDisplayName { get; set; }
        public Nullable<bool> IsApprovedOrRejected { get; set; }
        public string ApprovedOrRejectedDate { get; set; }
        public string ApprovedOrRejectedBy { get; set; }
        public string ApprovedOrRejectedRemark { get; set; }
        public bool IsOpenForModerations { get; set; }
        public Nullable<double> PaymentAmount { get; set; }
        public bool IsFinalApproved { get; set; }
        public bool IsActive { get; set; }
        public SemesterTimetableVM timetableRecords { get; set; }
        public bool canSendToApproval { get; set; }
    }
}