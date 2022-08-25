using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.ViewModels
{
    public class IntakeVM
    {
        public int IntakeId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string IntakeCode { get; set; }
        public string IntakeName { get; set; }
        public Nullable<int> IntakeYear { get; set; }
        public bool IsActive { get; set; }
    }
}