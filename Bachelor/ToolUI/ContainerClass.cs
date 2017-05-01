using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;

namespace ToolUI
{
    public class ContainerClass
    {
        public ICard card;
        public Model model;

        public Model getModel() { return model; }

        public ICard getCard() { return card; }

        public void setModel(Model newModel) { model = newModel; }

        public void setcard(ICard newCard) { card = newCard; }

        public ContainerClass(Model mod,ICard ca)
        {
            model = mod;
            card = ca;
        }

        
    }
}
