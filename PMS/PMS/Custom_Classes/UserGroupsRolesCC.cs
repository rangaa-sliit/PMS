using PMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Custom_Classes
{
    public class UserGroupsRolesCC
    {
        public List<AccessGroup> SelectedUserGroups { get; set; }
        public List<AspNetRoles> SelectedUserRoles { get; set; }
    }
}