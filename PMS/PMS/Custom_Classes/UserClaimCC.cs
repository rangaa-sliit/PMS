using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Custom_Classes
{
    public class UserClaimCC
    {
        public string UserId { get; set; }
        public List<int> SelectedUserGroupIds { get; set; }
        public string passingUserGroupIds { get; set; }
        public List<AccessGroup> UserGroups { get; set; }
        public List<string> SelectedUserRoleIds { get; set; }
        public string passingUserRoleIds { get; set; }
        public List<AspNetRoles> UserRoles { get; set; }
        public List<int> SelectedUserClaimIds { get; set; }
        public string passingUserClaimIds { get; set; }
        public List<Claim> Claims { get; set; }
    }
}