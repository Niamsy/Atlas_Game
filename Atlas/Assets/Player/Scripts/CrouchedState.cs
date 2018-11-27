using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;

namespace Player
{
    public class CrouchedState : State<PlayerController>
    {
        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _Actor.GetInput();
            _Actor.CheckForGrounded();
            _Actor.CheckForSprintInput();
            _Actor.Crouch();
            //_Actor.RotateAim();
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