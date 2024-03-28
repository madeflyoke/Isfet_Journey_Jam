using UnityEngine;

namespace GameStateMachine.States
{
    public class FinishState : BaseState
    {
        public FinishState(StateContext context) : base(context)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _stateContext.ScreenController.EnableFade();
            _stateContext.ScreenController.ShowWinScreen();
        }
    }
}
