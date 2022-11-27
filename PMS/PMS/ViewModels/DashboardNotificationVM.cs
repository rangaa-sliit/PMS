using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.ViewModels
{
    public class DashboardNotificationVM
    {
        public int NotificationId { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
        public string NotificationDate { get; set; }
        public bool IsActive { get; set; }
        public string NotificationType { get; set; }
    }
}