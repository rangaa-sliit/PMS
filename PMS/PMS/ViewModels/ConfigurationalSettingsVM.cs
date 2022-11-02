using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.ViewModels
{
    public class ConfigurationalSettingsVM
    {
        public int Id { get; set; }
        public string ConfigurationKey { get; set; }
        public bool IsFacultyWise { get; set; }
        public string FacultyName { get; set; }
        public string ConfigurationValue { get; set; }
        public bool IsActive { get; set; }
    }
}