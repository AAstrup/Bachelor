using Bachelor;
using System.Collections.Generic;

namespace GameEngine
{
    /// <summary>
    /// Cards does implement both ICard and ITrackable, and casting either way is always a possibility.
    /// The reason for having two interfaces, is because the CardTemplate has to implement ICard, but not ITrackable
    /// Thus we are not forced to have empty methods on the CardTemplate
    /// </summary>
    public interface ITrackable
    {
        void IncreaseTemplatesWins(Deck deck);
        void IncreaseTemplatesLoss(Deck deck);
        double GetWinLossRate();
        Deck GetBestDeck();
        HashSet<Deck> GetDecksWithThis();
        void AddDeck(Deck deck);
        int GetWins();
        int GetLosses();
        ICard GetTemplate();
        ITrackable GetTemplateAsTrackable();
        void DecreaseTemplateDominance(ICard copy);
        void RegisterCopy(ICard cardTracker);
        double GetDominance(ICard card);
    }
}