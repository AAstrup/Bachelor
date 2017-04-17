using System;

namespace GameEngine
{
    public class CardTracker : CardTemplate, ITrackable
    {
        private ICard templateAsCard;//If null, then this is the template
        private ITrackable templateAsTrackable;//If null, then this is the template
        int wins;
        int losses;

        public CardTracker():base() { }

        public CardTracker(ICard templateAsCard,ITrackable templateAsTrackable)
        {
            this.templateAsCard = templateAsCard;
            this.templateAsTrackable = templateAsTrackable;
        }

        public override void Win()
        {
            templateAsTrackable.IncreaseTemplatesWins();
        }
        public override void Loss()
        {
            templateAsTrackable.IncreaseTemplatesLoss();
        }

        public void IncreaseTemplatesWins()
        {
            wins++;
        }

        public void IncreaseTemplatesLoss()
        {
            losses++;
        }

        public double GetWinLossRate()
        {
            if (wins + losses == 0)
                return -1;
            double toreturn = ((Double)wins) / ((Double)(wins + losses));
            return toreturn * 100.0;
        }
    }
}