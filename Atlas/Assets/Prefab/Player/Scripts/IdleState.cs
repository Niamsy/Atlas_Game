using UnityEngine;
using StateMachine;

namespace Player
{
    public class IdleState : State<PlayerController>
    {
        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _Actor.CheckForGrounded();
            _Actor.GetInput();
            //_Actor.Walk();
            _Actor.CheckForSprintInput();
            _Actor.RotateAim();
            if (_Actor.CheckForCrouchedInput())
            {
                _Actor.ToggleCrouchedState();
            }
            if (_Actor.CheckForJumpInput())
                _Actor.Jump();
        }
    }
}
