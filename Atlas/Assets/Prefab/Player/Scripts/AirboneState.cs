using UnityEngine;
using StateMachine;

namespace Player
{
    public class AirboneState : State<PlayerController>
    {
        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _Actor.GetInput();
            _Actor.CheckForGrounded();
            _Actor.CheckForSprintInput();
        }
    }
}
