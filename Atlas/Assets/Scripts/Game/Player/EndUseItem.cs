using Player;
using StateMachine;
using UnityEngine;

public class EndUseItem : State<PlayerController>
{
    public override void OnSLStatePreExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateExit(animator, stateInfo, layerIndex);
        _Actor.UseItemValue = 0;
    }
}
