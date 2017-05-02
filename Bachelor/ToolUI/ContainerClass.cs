﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using Windows.UI.Xaml.Media;
using Windows.UI;

namespace ToolUI
{
    public class ContainerClass
    {
        public CardStats card;
        public Model model;
        public bool simulated {get; set;}

        public Model getModel() { return model; }

        public CardStats getCard() { return card; }

        public void setModel(Model newModel) { model = newModel; }

        public void setcard(CardStats newCard) { card = newCard; }


        public ContainerClass(Model mod,CardStats ca)
        {
            model = mod;
            card = ca;
            simulated = false;
        }

        public class rankCriteriaClass{
            
            public bool[] ands {get; set;}
            public bool[] ors { get; set; }
            public int[] winRatio { get; set; }
            public int[] domminance { get; set; }

            public rankCriteriaClass() {
                ands = new bool[5] { false, false, false, false, false};
                ors = new bool[5] { false, false, false, false, false };
            }

        }

    }
}