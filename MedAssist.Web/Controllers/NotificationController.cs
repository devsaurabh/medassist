using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MedAssist.Web.Models;
using MedAssist.Web.ViewModel;

namespace MedAssist.Web.Controllers
{
    public class NotificationController : ApiController
    {
        private readonly PatientContext _context;

        public NotificationController()
        {
            _context = new PatientContext();
        }

        public IHttpActionResult Get()
        {
            Patient patient = _context.Patients.FirstOrDefault();
            bool hasPendingNotification = false;

            if (patient != null)
            {
                hasPendingNotification = !patient.NotificationSent;
            }

            return Ok(hasPendingNotification);
        }

        public IHttpActionResult Post(NotificationViewModel notification)
        {
            Patient patient = _context.Patients.FirstOrDefault();

            if (patient != null && notification.Acknowledged)
            {
                patient.NotificationSent = true;
            }
            _context.SaveChanges();
            return Ok();
        }
    }
}
