using UnityEngine;
using StateMachine;

namespace Player {

    public class LocomotionState : State<PlayerController> {

        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _Actor.SetSpeedScale(_Actor.IsSprinting ? _Actor._SprintScale : 1f);
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
            _Actor.GetInput();
            _Actor.RotateAim();
            _Actor.CheckForSprintInput();
            //_Actor.Walk();
            _Actor.GoToIdleState(_Actor.CheckForIdle());
            if (_Actor.CheckForCrouchedInput())
            {
                _Actor.ToggleCrouchedState();
            } 
            if (_Actor.CheckForJumpInput())
            {
                _Actor.Jump();
            }
            if (_Actor.CheckForPickInput())
            {
                _Actor.Pick();
            }
        }
    }
}
