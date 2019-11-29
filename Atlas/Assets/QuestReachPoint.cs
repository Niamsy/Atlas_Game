using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Questing;
using Game.Item;

public class QuestReachPoint : MonoBehaviour
{
    [SerializeField] public ItemAbstract id;
    [SerializeField] private ConditionEvent _conditionEvent;
    [SerializeField] private Condition _raisedCondition;

    private void OnTriggerEnter(Collider other)
    {
        ItemAbstract tmpItem = new ItemAbstract();
        tmpItem.name = "reachpoint:" + id;
        _conditionEvent.Raise(_raisedCondition, tmpItem, 1);
        gameObject.SetActive(false);
    }

}
