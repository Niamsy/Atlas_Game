using Game.Item;

namespace Game.Questing
{
    public interface IConditionEventListener
    {
        void OnEventRaised(Condition condition, ItemAbstract item, int count);
    }
}