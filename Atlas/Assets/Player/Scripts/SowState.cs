using StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class SowState : State<PlayerController>
    {
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _Actor.SetSpeedScale(0);
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _Actor.GetInput();
            _Actor.CheckForGrounded();
        }
    }
}
