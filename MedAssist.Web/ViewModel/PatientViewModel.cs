using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedAssist.Web.ViewModel
{
    public class PatientViewModel
    {
        [Required(ErrorMessage = "Patient ExternalId is required")]
        public string ExternalId { get; set; }

        [Required(ErrorMessage = "Patient Name is required")]
        public string Name { get; set; }

        public int Age { get; set; }

        [Required(ErrorMessage = "Medicine Name is required")]
        public string MedicineName { get; set; }

        public List<string> Intervals { get; set; }
    }

    public class NotificationViewModel
    {
        public bool Acknowledged { get; set; }
    }
}