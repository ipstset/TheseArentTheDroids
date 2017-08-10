using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Ipstset.Droids
{
    public class GameResult
    {
        public string Id { get; set; }
        public int GameTypeId { get; set; } 
        public List<Card> Cards { get; set; }
        public Player Player { get; set; }
        public string GameMessage { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime Date { get; set; }
        public bool GameOver { get; set; }
        public bool PlayerWon { get; set; }
    }
}
