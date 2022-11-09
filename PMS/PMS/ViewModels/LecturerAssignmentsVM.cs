using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.ViewModels
{
    public class LecturerAssignmentsVM
    {
        public int Id { get; set; }
        public int SemesterId { get; set; }
        public string LecturerName { get; set; }
        public string SubjectName { get; set; }
        public string StudentBatches { get; set; }
        public bool IsActive { get; set; }
    }
}