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
        [Required(ErrorMessage = "Campus is required")]
        public int CampusId { get; set; }
        public Campus Campus { get; set; }
        [MaxLength(150, ErrorMessage = "Maximum 150 characters exceeded")]
        public string Building { get; set; }
        [MaxLength(150, ErrorMessage = "Maximum 150 characters exceeded")]
        public string Floor { get; set; }
        [MaxLength(150, ErrorMessage = "Maximum 150 characters exceeded")]
        public string HallName { get; set; }
        public bool IsActive { get; set; }
    }
}