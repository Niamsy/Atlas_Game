namespace Game.Questing
{
    public interface IConditionEventListener
    {
        void OnEventRaised(Condition condition);
    }
}