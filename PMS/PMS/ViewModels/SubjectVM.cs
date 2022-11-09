using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.ViewModels
{
    public class SubjectVM
    {
        public int SubjectId { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public string DegreeOrCommon { get; set; }
        public bool IsActive { get; set; }
    }
}