using AppointmentScheduler.Models.Attributes;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppointmentScheduler.Models
{
	public class AppointmentModel
	{
		

		/// <summary>
		/// Patient's Full Name: I.E. Caleb Overmyer
		/// </summary>
		[Required]
		[DisplayName("Patient's Full Name")]
		[StringLength(20, MinimumLength = 4)]
		public string PatientName { get; set; }

		[Required]
		[DisplayName("Email Address")]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[DisplayName("Phone Number")]
		[RegularExpression(@"^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$",
			ErrorMessage = "Please enter a valid 10-digit phone number.")]
		public string PhoneNumber { get; set; }


		/// <summary>
		/// DateTime Object for Appointment Request
		/// </summary>
		[Required]
		[DisplayName("Appointment Request Date and Time")]
		[DataType(DataType.Date)]
		public DateTime AppointmentTime { get; set; }

		/// <summary>
		/// Approximate Net Worth of Patient: I.E. -15000.00 (I am in excruciating student loan debt)
		/// </summary>
		[Required]
		[DisplayName("Patient's Approximate Net Worth")]
		[DataType(DataType.Currency)]
		[MinValue(90000, ErrorMessage="Doctors refuse to see patients unless their net worth is greater than 90K.")]
		public decimal PatientNetWorth { get; set; }

		/// <summary>
		/// Primary Care Doctor's Last Name: I.E. Smith
		/// </summary>
		[Required]
		[DisplayName("Primary Care Doctor's Last Name")]
		public string DoctorName { get; set; }

		/// <summary>
		/// Level of Pain from 1-10: I.E. 7
		/// </summary>
		[Required]
		[DisplayName("Level of Pain (1-10)")]
		[Range(1, 10)]
		[MinValue(6, ErrorMessage="Doctors refuse to see patients below a 5 on the pain scale")]
		public int PainLevel { get; set; }

		/// ADDRESS
		[Required]
		[DisplayName("Address Line 1")]
		public string AddressLine1 { get; set; }

		[ValidateNever]
		[DisplayName("Address Line 2")]
		public string AddressLine2 { get; set; }

		[Required]
		public string City { get; set; }

		[Required]
		public string State { get; set; }

		[Required]
		[DisplayName("Zip Code")]
		public string ZipCode { get; set; }

		public AppointmentModel(string patientName, string email, string phoneNumber, DateTime appointmentTime, decimal patientNetWorth, string doctorName, int painLevel, string addressLine1, string addressLine2, string city, string state, string zipCode)
		{
			PatientName = patientName;
			Email = email;
			PhoneNumber = phoneNumber;
			AppointmentTime = appointmentTime;
			PatientNetWorth = patientNetWorth;
			DoctorName = doctorName;
			PainLevel = painLevel;
			AddressLine1 = addressLine1;
			AddressLine2 = addressLine2;
			City = city;
			State = state;
			ZipCode = zipCode;
		}

		public AppointmentModel()
		{
			AddressLine2 = "";
		}
	}
}
