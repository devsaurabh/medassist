using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;

namespace MedAssist.Web.Models
{
    public class PatientContext : DbContext
    {
        public PatientContext()
            : base("MedAssistConnStr")
        {
            Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Dosage> Dosages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().HasMany(t => t.Dosages).WithRequired(t => t.Patient)
                .HasForeignKey(t => t.PatientId);

            modelBuilder.Entity<Dosage>().HasRequired(t => t.Patient).WithMany(t => t.Dosages);

            modelBuilder.Entity<Dosage>().HasRequired(t => t.Medicine).WithMany(t => t.Dosages);

            base.OnModelCreating(modelBuilder);
        }
    }

    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        [Required]
        public string ExternalId { get; set; }

        [Required]
        public string Name { get; set; }

        public int Age { get; set; }

        public bool NotificationSent { get; set; }

        public virtual ICollection<Dosage> Dosages { get; set; }
    }

    public class Medicine
    {
        [Key]
        public int MedicineId { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Dosage> Dosages { get; set; }
    }

    public class Dosage
    {
        [Key]
        public int DosageId { get; set; }

        public int PatientId { get; set; }

        public virtual Patient Patient { get; set; }

        public int MedicineId { get; set; }

        public virtual Medicine Medicine { get; set; }

        public string Interval { get; set; }
    }
}