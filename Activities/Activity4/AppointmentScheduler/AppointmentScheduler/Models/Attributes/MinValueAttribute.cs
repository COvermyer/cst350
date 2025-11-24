using System.ComponentModel.DataAnnotations;

namespace AppointmentScheduler.Models.Attributes
{
	/// <summary>
	/// Custom ValidationAttribute to set a minimim value for numeric properties
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public class MinValueAttribute : ValidationAttribute
	{
		private readonly decimal _minValue;

		public MinValueAttribute(double minValue)
		{
			_minValue = (decimal)minValue;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value == null)
				return ValidationResult.Success; // null values are handled by [Required] if needed

			try
			{
				var decimalValue = Convert.ToDecimal(value);
				if (decimalValue < _minValue)
				{
					var message = ErrorMessage ?? $"{validationContext.DisplayName} must be at least {_minValue}.";
					return new ValidationResult(message); // failure
				}
			}
			catch (Exception) { } // FIXME: Ignore conversion errors for now

            // passed all validation results
            return ValidationResult.Success;
		}
	}
}
