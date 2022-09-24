using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Custom_Classes
{
    public class ClaimCC
    {
        public int ClaimId { get; set; }
        public string ClaimName { get; set; }
        public string ClaimValue { get; set; }
        public List<string> SelectedClaimValues { get; set; }
        public string SubOperation { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}