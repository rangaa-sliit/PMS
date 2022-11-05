using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.ViewModels
{
    public class SemesterSubjectLICVM
    {
        public int SSLICId { get; set; }
        public string SemesterSubjectName { get; set; }
        public string LICName { get; set; }
        public bool IsActive { get; set; }
    }
}