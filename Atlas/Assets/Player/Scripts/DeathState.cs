using UnityEngine;
using StateMachine;

namespace Player
{
    public class DeathState : State<PlayerController>
    {
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _Actor.SetSpeedScale(0);
            DoLogic();
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            DoLogic(); 
        }

        public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            DoLogic();
        }

        private void DoLogic()
        {
            _Actor.CheckForGrounded();
            _Actor.CheckForDeath();
            if (_Actor.IsDead == false)
            {
                _Actor.GoToIdleState(true);
            }
        }
    }
}


