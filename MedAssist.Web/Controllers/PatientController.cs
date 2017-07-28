using System.Collections.Generic;
using MedAssist.Web.Models;
using System.Linq;
using System.Web.Http;
using MedAssist.Web.ViewModel;

namespace MedAssist.Web.Controllers
{
    public class PatientController : ApiController
    {
        private readonly PatientContext _patientContext;

        public PatientController()
        {
            _patientContext = new PatientContext();
        }

        public IHttpActionResult Post(PatientViewModel model)
        {
            
            Medicine existingMedicine = _patientContext.Medicines.FirstOrDefault(t => t.Name == model.MedicineName);

            if (existingMedicine == null)
            {
                existingMedicine = new Medicine
                {
                    Name = model.MedicineName,
                };

                _patientContext.Medicines.Add(existingMedicine);
            }

            Patient existingPatient = _patientContext.Patients.FirstOrDefault(t => t.ExternalId == model.ExternalId);

            // if no patient is found, create a new patient
            if (existingPatient == null)
            {
                existingPatient = new Patient
                {
                    ExternalId = model.ExternalId,
                    Name = model.Name,
                    Age = model.Age,
                    NotificationSent = false
                };

                _patientContext.Patients.Add(existingPatient);
            }

            List<Dosage> existingDosages = _patientContext.Dosages.Where(t => t.MedicineId == existingMedicine.MedicineId &&
                                                        t.PatientId == existingPatient.PatientId).ToList();

            if (existingDosages.Any())
            {
                _patientContext.Dosages.RemoveRange(existingDosages);
            }


            existingDosages = model.Intervals.Select(t => new Dosage
            {
                MedicineId = existingMedicine.MedicineId,
                PatientId = existingPatient.PatientId,
                Interval = t
            }).ToList();

            _patientContext.Dosages.AddRange(existingDosages);

            _patientContext.SaveChanges();

            return Ok();
        }

        public IHttpActionResult Get()
        {
            Patient patient = _patientContext.Patients.First();
            
            List<DosageViewModel> dosages = patient.Dosages.Select(patientDosage => new DosageViewModel
                {
                    MedicineName = patientDosage.Medicine.Name,
                    PatientName = patientDosage.Patient.Name,
                    Interval = patientDosage.Interval
                })
                .ToList();

            return Ok(dosages);
        }
    }
}