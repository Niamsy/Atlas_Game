using UnityEngine;
using StateMachine;
using Player;

namespace InputManagement {

    public class GroundedState : State<PlayerController> {

        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            DoLogic();
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            DoLogic();
        }

        public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }

        private void DoLogic()
        {
            //_Actor.CheckForGrounded();
            _Actor.GetInput();
            _Actor.RotateAim();
            _Actor.GroundedHorizontalMovement(true);
            if (_Actor.CheckForIdle())
                _Actor.GoToIdleState();
            if (_Actor.CheckForJumpInput())
                _Actor.Jump();
        }
    }
}
