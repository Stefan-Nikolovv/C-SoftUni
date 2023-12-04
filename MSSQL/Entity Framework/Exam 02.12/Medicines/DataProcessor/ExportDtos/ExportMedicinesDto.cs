

using Medicines.Data.Models;
using Newtonsoft.Json;

namespace Medicines.DataProcessor.ExportDtos
{
    public class ExportMedicinesDto
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Price")]
        public string Price { get; set; }
        [JsonProperty("Pharmacy")]
        public Pharmacy Pharmacy { get; set; }

    }
}



