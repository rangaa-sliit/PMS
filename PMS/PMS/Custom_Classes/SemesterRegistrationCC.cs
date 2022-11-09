using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Custom_Classes
{
    public class SemesterRegistrationCC
    {
        public int FacultyId { get; set; }
        public int InstituteId { get; set; }
        public HttpPostedFileBase UploadedFile { get; set; }
    }
}