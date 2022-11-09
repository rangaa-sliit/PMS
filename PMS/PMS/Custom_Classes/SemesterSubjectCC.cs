using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Custom_Classes
{
    public class SemesterSubjectCC
    {
        public int SemesterId { get; set; }
        public Nullable<int> CalendarYear { get; set; }
        public string CalendarPeriodName { get; set; }
        public Nullable<int> IntakeYear { get; set; }
        public string IntakeName { get; set; }
        public Nullable<int> AcademicYear { get; set; }
        public Nullable<int> AcademicSemester { get; set; }
        public string FacultyName { get; set; }
        public string InstituteName { get; set; }
        public string DegreeName { get; set; }
        public string SpecializationName { get; set; }
        public List<Subject> SubjectList { get; set; }
        public List<int> ViewingSemesterSubjectIdList { get; set; }
        public string PassingSemesterSubjectIdList { get; set; }
    }
}