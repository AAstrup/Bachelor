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
        public int orgCost = 0;
        public int orgAttack = 0;
        public int orghealth = 0;

        public int getOrgCost() { return orgCost; }
        public int getOrgAttack() { return orgAttack; }
        public int getOrghealth() { return orghealth; }

        public ICard card { get; set; }
        public string rank { get; set; }
        public bool simulated { get; set; }
        public bool changed { get; set; }
        public int win_ratio { get; set; }
        public double domminance { get; set; }

        public int numberOfFights { get; set; }

        public CardStats(ICard car) { card = car; rank = "B"; orgCost = card.GetCost(); orgAttack = card.GetDamage(); orghealth = card.GetMaxHp(); }
    }
}