using UnityEngine;
using Game.Questing;
using Game.Item;

public class QuestReachPoint : MonoBehaviour
{
    [SerializeField] public ItemAbstract id = null;
    [SerializeField] private ConditionEvent _conditionEvent = null;
    [SerializeField] private Condition _raisedCondition = null;

    private void OnTriggerEnter(Collider other)
    {
        _conditionEvent.Raise(_raisedCondition, id, 1);
        gameObject.SetActive(false);
    }

}
