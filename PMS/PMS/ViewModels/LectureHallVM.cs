//Developed By:- Ranga Athapaththu
//Developed On:- 2022/08/19

using PMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PMS.ViewModels
{
    public class LectureHallVM
    {
        public int HallId { get; set; }
        public int CampusId { get; set; }
        public string CampusName { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public string HallName { get; set; }
        public bool IsActive { get; set; }
    }
}