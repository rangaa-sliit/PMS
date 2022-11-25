using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Custom_Classes
{
    public class RoleClaim_ClaimCC
    {
        public string RoleId { get; set; }
        public int AccessGroupClaimId { get; set; }
        public string ClaimName { get; set; }
    }
}