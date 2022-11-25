using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Custom_Classes
{
    public class AccessGroupRoleClaimsCC
    {
        public string RoleId { get; set; }
        public int AccessGroupId { get; set; }
        public string RoleName { get; set; }
        public string AccessGroupName { get; set; }
        public string passingRoleClaimIds { get; set; }
        public List<AccessGroupClaim_ClaimCC> ClaimsList { get; set; }
        public List<AccessGroupClaim_ClaimCC> SelectedRoleClaims { get; set; }
    }
}