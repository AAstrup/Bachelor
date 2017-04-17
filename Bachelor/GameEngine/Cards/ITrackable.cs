namespace GameEngine
{
    /// <summary>
    /// Cards does implement both ICard and ITrackable, and casting either way is always a possibility.
    /// The reason for having two interfaces, is because the CardTemplate has to implement ICard, but not ITrackable
    /// Thus we are not forced to have empty methods on the CardTemplate
    /// </summary>
    public interface ITrackable
    {
        void IncreaseTemplatesWins();
        void IncreaseTemplatesLoss();
        double GetWinLossRate();
    }
}