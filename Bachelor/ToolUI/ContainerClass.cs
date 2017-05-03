using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using Windows.UI.Xaml.Media;
using Windows.UI;
using Bachelor;

namespace ToolUI
{
    public class ContainerClass
    {
        public CardStats card;
        public Model model;
        public bool simulated { get; set; }

        public Model getModel() { return model; }

        public CardStats getCard() { return card; }

        public void setModel(Model newModel) { model = newModel; }

        public void setcard(CardStats newCard) { card = newCard; }

        public List<Deck> resultDecks { get; set; }

        public RankCriteria rankCriteria { get; set;}


        public ContainerClass(Model mod,CardStats ca)
        {
            model = mod;
            card = ca;
            simulated = false;
            resultDecks = null;
            rankCriteria = null;
        }

        public class RankCriteria{
            
            public bool[] ands {get; set;}
            public bool[] ors { get; set; }
            public bool[] winRatio_t { get; set; }
            public bool[] domminance_t { get; set; }
            public int[] winRatio { get; set; }
            public int[] domminance { get; set; }

            public RankCriteria() {
                ands = new bool[5] { true, true, true, true, true };
                ors = new bool[5] { false, false, false, false, false };
                winRatio_t = new bool[5] { true, true, true, true, true };
                domminance_t = new bool[5] { false, false, false, false, false };

                winRatio = new int[5] { 70, 60, 40, 20, 0 };
                domminance = new int[5] { 1, 2, 3, 4, 5 };
            }

            public string evaluateCard(CardStats card){
                //RANK S
                if (ands[0])
                {
                    if (
                        ((winRatio_t[0] && winRatio[0] < card.win_ratio) || !winRatio_t[0]) 
                        &&
                        ((domminance_t[0] && domminance[0] >= (Convert.ToInt32(card.domminance))) || !domminance_t[0])
                        ) {
                        return "S"; }
                }
                else if (
                        ((winRatio_t[0] && winRatio[0] < card.win_ratio) || !winRatio_t[0])
                        ||
                        ((domminance_t[0] && domminance[0] >= (Convert.ToInt32(card.domminance))) || !domminance_t[0])
                        )
                {
                    return "S"; }

                //RANK A
                if (ands[1])
                {
                    if (
                        ((winRatio_t[1] && winRatio[1] < card.win_ratio && winRatio[2] >= card.win_ratio) || !winRatio_t[1])
                        &&
                        ((domminance_t[1] && domminance[1] < (Convert.ToInt32(card.domminance))) || !domminance_t[1])
                        ) { return "A"; }
                }
                else if (
                        ((winRatio_t[1] && winRatio[1] < card.win_ratio) || !winRatio_t[1])
                        ||
                        ((domminance_t[1] && domminance[1] >= (Convert.ToInt32(card.domminance))) || !domminance_t[1])
                        )
                { return "A"; }

                //RANK B
                if (ands[2])
                {
                    /*
                    if ((winRatio_t[2] && winRatio[2] < card.win_ratio) || !winRatio_t[2])
                    {
                        if ((domminance_t[2] && domminance[2] >= (Convert.ToInt32(card.domminance))) || !domminance_t[2])
                        {
                            return "B";
                        }
                    }
                    */
                    if (
                        ((winRatio_t[2] && winRatio[2] < card.win_ratio) || !winRatio_t[2])
                        &&
                        ((domminance_t[2] && domminance[2] >= (Convert.ToInt32(card.domminance))) || !domminance_t[2])
                        )
                    {
                        return "B";
                    }
                }   
                else if (
                        ((winRatio_t[2] && winRatio[2] < card.win_ratio) || !winRatio_t[2])
                        ||
                        ((domminance_t[2] && domminance[2] >= (Convert.ToInt32(card.domminance))) || !domminance_t[2])
                        )
                {
                    return "B"; }

                //RANK C
                if (ands[3])
                {
                    if (
                        ((winRatio_t[3] && winRatio[3] < card.win_ratio) || !winRatio_t[3])
                        &&
                        ((domminance_t[3] && domminance[3] >= (Convert.ToInt32(card.domminance))) || !domminance_t[3])
                        ) { return "C"; }
                }
                else if (
                        ((winRatio_t[3] && winRatio[3] >= card.win_ratio) || !winRatio_t[3])
                        ||
                        ((domminance_t[3] && domminance[3] < (Convert.ToInt32(card.domminance))) || !domminance_t[3])
                        )
                { return "C"; }

                //RANK F
                if (ands[4])
                {
                    if (
                        ((winRatio_t[4] && winRatio[4] < card.win_ratio) || !winRatio_t[4])
                        &&
                        ((domminance_t[4] && domminance[4] >= (Convert.ToInt32(card.domminance))) || !domminance_t[4])
                        ) {
                        return "F"; }
                }
                else if (
                        ((winRatio_t[4] && winRatio[4] < card.win_ratio) || !winRatio_t[4])
                        ||
                        ((domminance_t[4] && domminance[4] >= (Convert.ToInt32(card.domminance))) || !domminance_t[4])
                        )
                {
                    return "F"; }

                return "F";
            }

        }

    }
}
