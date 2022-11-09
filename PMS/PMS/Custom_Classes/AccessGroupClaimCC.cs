using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Custom_Classes
{
    public class AccessGroupClaimCC
    {
        public int AccessGroupId { get; set; }
        public string AccessGroupName { get; set; }
        public string passingAccessGroupClaimIds { get; set; }
        public List<Claim> ClaimsList { get; set; }
        public List<Claim> SelectedAccessGroupClaims { get; set; }
    }
}