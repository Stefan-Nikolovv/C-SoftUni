

using System.ComponentModel.DataAnnotations;
using Medicines.Common;

namespace Medicines.DataProcessor.ImportDtos
{
    public class ImportPatientDto
    {
        [Required]
        [MinLength(ValidationConstants.PatientFullNameMinLength)]
        [MaxLength(ValidationConstants.PatientFullNameMaxLength)]
        public string FullName { get; set; }
        [Required]
        [Range(0, 2)]
        public int AgeGroup { get; set; }
        [Required]
        [Range(0, 1)]
        public int Gender { get; set; }
        public int[] Medicines { get; set; }
    }
}



