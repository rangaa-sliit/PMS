using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.ViewModels
{
    public class SemesterRegistrationVM
    {
        public int SemesterId { get; set; }
        public Nullable<int> CalendarYear { get; set; }
        public string CalendarPeriodName { get; set; }
        public Nullable<int> IntakeYear { get; set; }
        public string IntakeName { get; set; }
        public Nullable<int> AcadamicYear { get; set; }
        public Nullable<int> AcadamicSemester { get; set; }
        public string FacultyName { get; set; }
        public string InstituteName { get; set; }
        public string DegreeName { get; set; }
        public string SpecializationName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public bool IsActive { get; set; }
    }
}