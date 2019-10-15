namespace Game.Questing
{
    public interface IQuestingEventListener
    {
        void OnEventRaised(Quest quest);
    }
}