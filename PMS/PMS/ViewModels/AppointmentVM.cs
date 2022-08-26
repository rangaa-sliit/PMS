using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.ViewModels
{
    public class AppointmentVM
    {
        public int AppointmentId { get; set; }
        public string UserId { get; set; }
        public string EmployeeName { get; set; }
        public int AppointmentTypeId { get; set; }
        public string AppointmentTypeName { get; set; }
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        public string AppointmentFrom { get; set; }
        public string AppointmentTo { get; set; }
        public bool IsActive { get; set; }
    }
}