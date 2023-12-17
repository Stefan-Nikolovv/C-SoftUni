namespace Medicines.DataProcessor
{
    using System.Globalization;
    using Medicines.Data;
    using Medicines.Data.Models.Enums;
    using Medicines.DataProcessor.ExportDtos;
    using Medicines.Extensions;
    using Medicines.Utilities;

    public class Serializer
    {
        private static XmlHelper xmlHelper;
        public static string ExportPatientsWithTheirMedicines(MedicinesContext context, string date)
        {
            xmlHelper = new();

            var medicine = context.Patients
                .Where(p => p.PatientsMedicines.Any(pm => p.Id == pm.MedicineId))
                .Select(p => new ExpoerPatinetsDto()
                {
                    Name = p.FullName,
                    AgeGroup = p.AgeGroup.ToString(),
                    Gender = p.Gender.ToString(),
                    ExportMedicines = p.PatientsMedicines
                    .Select(pm => new ExportMedicineDto()
                    {
                        Name = pm.Medicine.Name,
                        Category = pm.Medicine.Category.ToString(),
                        Price = pm.Medicine.Price.ToString("0.00"),
                        Producer = pm.Medicine.Producer,
                        BestBefore = DateTime.Parse(pm.Medicine.ExpiryDate.ToString("d", CultureInfo.InvariantCulture)),
                    })
                    .OrderByDescending(p => p.BestBefore)
                    .ThenBy(p => p.Price)
                    .ToArray()

                })
                .OrderByDescending(p => p.ExportMedicines.Length)
                .ThenBy(p => p.Name)
                .ToArray();

            return xmlHelper.Serialize(medicine, "Patients");
        }

        public static string ExportMedicinesFromDesiredCategoryInNonStopPharmacies(MedicinesContext context, int medicineCategory)
        {
            var medicamnets = context.Medicines
                .Where(m => m.Category == (Category)medicineCategory)
                .Select(m => new ExportMedicinesDto()
                {
                    Name = m.Name,
                    Price = m.Price.ToString("0.00"),
                    Pharmacy = new Data.Models.Pharmacy()
                    {
                        Name = m.Pharmacy.Name,
                        PhoneNumber = m.Pharmacy.PhoneNumber,
                    }
                })
                .OrderBy(m => m.Name)
                .ThenBy(m => m.Price)
                .ToArray();
            return medicamnets.SerializeToJson();
        }
    }
}
