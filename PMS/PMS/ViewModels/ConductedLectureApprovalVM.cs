using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.ViewModels
{
    public class ConductedLectureApprovalVM
    {
        public string LecturerId { get; set; }
        public string LecturerName { get; set; }
        public string RecordMonth { get; set; }
        public int PendingRecordsCount { get; set; }
        public int ApprovedRecordsCount { get; set; }
        public int RejectedRecordsCount { get; set; }
    }
}