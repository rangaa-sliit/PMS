using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Custom_Classes
{
    public class LecturerMonthlyApprovalCC
    {
        public string LecturerId { get; set; }
        public List<int> CLIdList { get; set; }
        public string Remark { get; set; }
    }
}