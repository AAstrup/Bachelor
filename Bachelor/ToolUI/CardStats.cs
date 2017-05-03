using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;

namespace ToolUI
{
    public class CardStats
    {
        public ICard card { get; set; }
        public string rank { get; set; }
        public bool simulated { get; set; }
        public bool changed { get; set; }
        public int win_ratio { get; set; }
        public double domminance { get; set; }

        public int numberOfFights { get; set; }

        public CardStats(ICard car) { card = car; rank = "B"; }
    }
}
