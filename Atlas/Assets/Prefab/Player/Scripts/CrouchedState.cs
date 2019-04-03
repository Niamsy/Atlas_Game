using UnityEngine;
using StateMachine;

namespace Player
{
    public class CrouchedState : State<PlayerController>
    {
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _Actor.SetSpeedScale(_Actor._CrouchScale);
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _Actor.GetInput();
            _Actor.CheckForGrounded();
            _Actor.CheckForSprintInput();
            //_Actor.Crouch();
            _Actor.RotateAim();
            if (_Actor.CheckForCrouchedInput())
            {
                _Actor.ToggleCrouchedState();
            }
            if (_Actor.CheckForPronedInput())
            {
                _Actor.TogglePronedState();
            }
        } 
    }
}