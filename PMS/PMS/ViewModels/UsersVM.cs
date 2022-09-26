/*Developed By:- Dulanjalee Wickremasinghe
    Developed On:- 2022/08/31 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.ViewModels
{
    public class UsersVM
    {
        public string Id { get; set; }
        public string TitleName { get; set; }
        public string FacultyName { get; set; }
        public bool IsActive { get; set; }
    }
}