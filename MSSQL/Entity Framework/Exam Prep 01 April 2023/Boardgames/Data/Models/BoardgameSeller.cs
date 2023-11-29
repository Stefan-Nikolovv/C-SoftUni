using System.ComponentModel.DataAnnotations.Schema;

namespace Boardgames.Data.Models
{
    public class BoardgameSeller
    {
        [ForeignKey(nameof(BoardgameId))]
        public int BoardgameId { get; set; }
        public Boardgame Boardgame { get; set; } = null!;
        [ForeignKey(nameof(SellerId))]
        public int SellerId { get; set; }
        public Seller Seller { get; set; } = null!;

    }
}