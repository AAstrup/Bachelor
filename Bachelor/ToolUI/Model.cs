using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;

namespace ToolUI
{
    public class Model
    {
        public List<ICard> cardsToDisplay = new List<ICard>() { new GameEngine.Card_Shadow_Rager(), new GameEngine.Card_Yeti(), new GameEngine.Card_Dr_Boom(), new GameEngine.Card_Earthen_Ring_Farseer(), new GameEngine.Card_Wisp() };

        public List<ICard> getCardsToDisplay() { return cardsToDisplay; }

        public void setCardsToDisplay(List<ICard> newCards) { cardsToDisplay = newCards; }

        public Model() { }

    }
}
