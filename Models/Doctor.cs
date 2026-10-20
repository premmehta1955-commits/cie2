
namespace HospitalAppointmentSystem.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public string DoctorName { get; set; } = "";
        public string Specialization { get; set; } = "";
        public int Experience { get; set; }
        public decimal ConsultationFee { get; set; }
    }

    public static class DoctorExtensions
    {
        public static decimal CalculateTotalCharge(this Doctor doctor)
        {
            return doctor.ConsultationFee + 100m;
        }
    }
}
