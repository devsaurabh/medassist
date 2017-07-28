namespace MedAssist.Web.ViewModel
{
    public class DosageViewModel
    {
        public string PatientName { get; set; }
        public string MedicineName { get; set; }
        public string Interval { get; set; }

        public string Message
        {
            get { return string.Format("Hello {0}. Your medicine {1} is due", PatientName, MedicineName); }
        }
    }
}