

using Boardgames.Commons;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Boardgames.DataProcessor.ExportDto
{
    public class ExportSellersDto
    {
        
        public string Name { get; set; }
        public string Website { get; set; }
        public ExporBoardGames[] Boardgames { get; set; }
    }
}
