using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

namespace Player
{
    public class PronedState : State<PlayerController>
    {
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _Actor.SetSpeedScale(_Actor._ProneScale);
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _Actor.GetInput();
            _Actor.CheckForGrounded();
            _Actor.CheckForSprintInput();
            //_Actor.Prone();
            _Actor.RotateAim();
            if (_Actor.CheckForPronedInput())
            {
                _Actor.TogglePronedState();
                _Actor.ToggleCrouchedState();
            }
        }
    }
}