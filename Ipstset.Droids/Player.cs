using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipstset.Droids
{
    public class Player
    {
        public string Id { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Streak { get; set; }
        public int LongestStreak { get; set; }

        public string WinPct
        {
            get { 
                var winPct = "";
                if (Wins + Losses != 0)
                {
                    double x = Wins/(double)(Wins + Losses);
                    winPct = String.Format("{0:#,0.000}",Math.Round(x, 3));
                    //String.Format("{0:#,0.000}", value)
                }
                return winPct;
            }
        }
    }
}
