using UnityEngine;
using StateMachine;

namespace Player {

    public class LocomotionState : State<PlayerController> {

        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _Actor.SetSpeedScale(1f);
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
            _Actor.CheckForGrounded();
            if (_Actor.CheckForDeath())
                return;
            _Actor.GetInput();
            _Actor.RotateAim();
            _Actor.CheckForPickInput();
            _Actor.CheckForSprintInput();
            //_Actor.Walk();
            _Actor.GoToIdleState(_Actor.CheckForIdle());
            if (_Actor.CheckForJumpInput())
                _Actor.Jump();
        }
    }
}
