using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.ViewModels
{
    public class SemesterTimetableVM
    {
        public int TimetableId { get; set; }
        public int SemesterSubjectId { get; set; }
        public string SubjectName { get; set; }
        public string LectureDate { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string Location { get; set; }
        public string LectureTypeName { get; set; }
        public string LecturerName { get; set; }
        public string StudentBatches { get; set; }
        public bool IsActive { get; set; }
        public bool IsLectureRecordAdded { get; set; }
        public string Comment { get; set; }
        public string DepartmentName { get; set; }
        public string FacultyName { get; set; }
        public string DegreeName { get; set; }
        public AppointmentType AppointmentTypeDetails { get; set; }
    }
}