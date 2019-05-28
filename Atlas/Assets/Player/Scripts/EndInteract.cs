using Player;
using StateMachine;
using UnityEngine;

public class EndInteract : State<PlayerController>
{
    public override void OnSLStatePreExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateExit(animator, stateInfo, layerIndex);
        _Actor.InteractValue = (int)AInteractable.InteractType.none;
    }
}
