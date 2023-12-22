namespace Medicines.DataProcessor
{
    using Medicines.Data;
    using Medicines.Data.Models;
    using Medicines.Data.Models.Enums;
    using Medicines.DataProcessor.ImportDtos;
    using Medicines.Utilities;
    using Microsoft.EntityFrameworkCore.Update;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid Data!";
        private const string SuccessfullyImportedPharmacy = "Successfully imported pharmacy - {0} with {1} medicines.";
        private const string SuccessfullyImportedPatient = "Successfully imported patient - {0} with {1} medicines.";

        private static XmlHelper xmlHelper;


        public static string ImportPatients(MedicinesContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            ImportPatientDto[] importPatientDtos = JsonConvert.DeserializeObject<ImportPatientDto[]>(jsonString);

            ICollection<Patient> patients = new HashSet<Patient>();
            var medicamentIds = context.Medicines
                .Select(x => x.Id)
                .ToArray();

            foreach (var importPatientDto in importPatientDtos)
            {
                if (!IsValid(importPatientDto))
                {

                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Patient patient = new Patient()
                {
                    FullName = importPatientDto.FullName,
                    AgeGroup = (AgeGroup)importPatientDto.AgeGroup,
                    Gender = (Gender)importPatientDto.Gender,
                };
                foreach (var id in importPatientDto.Medicines)
                {
                    if(patient.PatientsMedicines.Any(pm => pm.MedicineId == id))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    PatientMedicine patientMedicine = new PatientMedicine()
                    {
                        Patient = patient,
                        MedicineId = id
                    };

                    patient.PatientsMedicines.Add(patientMedicine);
                }
                patients.Add(patient);
                sb.AppendLine(String.Format(SuccessfullyImportedPatient, patient.FullName, patient.PatientsMedicines.Count));
            }
            context.Patients.AddRange(patients);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportPharmacies(MedicinesContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            xmlHelper = new XmlHelper();

            ImportPharmacyDto[] importPharmacyDtos = xmlHelper.Deserialize<ImportPharmacyDto[]>(xmlString, "Pharmacies");
            

            ICollection<Pharmacy> pharmacies = new HashSet<Pharmacy>();
            DateTime parsedDateTime;
            
            foreach (var importPharmacyDto in importPharmacyDtos)
            {
                if (!IsValid(importPharmacyDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                if(importPharmacyDto.IsNonStop != "true" && importPharmacyDto.IsNonStop != "false")
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                Pharmacy pharmacy = new Pharmacy()
                {
                    Name = importPharmacyDto.Name,
                    PhoneNumber = importPharmacyDto.PhoneNumber,
                    IsNonStop = Boolean.Parse(importPharmacyDto.IsNonStop)
                    
                };

                foreach (var medicineItem in importPharmacyDto.ImportMedicines)
                {
                    if (!IsValid(medicineItem))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    DateTime medicineProductionDate;
                    bool ismedicineContractStartDateValid = DateTime.TryParseExact(medicineItem.ProductionDate,
                        "yyyy-MM-dd", CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out medicineProductionDate);
                    if (!ismedicineContractStartDateValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime medicineExpiryDate;
                    bool ismedicineContractEndDateValid = DateTime.TryParseExact(medicineItem.ExpiryDate,
                        "yyyy-MM-dd", CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out medicineExpiryDate);
                    if (!ismedicineContractEndDateValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (medicineProductionDate >= medicineExpiryDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    bool pharmacyName;
                    pharmacyName = pharmacy.Medicines.Any(m => m.Name == medicineItem.Name);
                    bool pharmacyMedicament;

                    pharmacyMedicament = pharmacy.Medicines.Any(m => m.Producer == medicineItem.Producer);

                    if (pharmacyName  && pharmacyMedicament)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    if(pharmacyName == false)
                    {
                        pharmacy.Medicines.Add(new Medicine()
                        {
                            Name = medicineItem.Name,
                            Price = medicineItem.Price,
                            Category = (Category)medicineItem.Category,
                            ProductionDate = medicineProductionDate,
                            ExpiryDate = medicineExpiryDate,
                            Producer = medicineItem.Producer,
                        });
                    }
                    
                    
                }
                pharmacies.Add(pharmacy);
                sb.AppendLine(String.Format(SuccessfullyImportedPharmacy,pharmacy.Name, pharmacy.Medicines.Count));

            }
            context.Pharmacies.AddRange(pharmacies);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
