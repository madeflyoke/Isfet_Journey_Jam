using UnityEngine;

namespace GameStateMachine.States
{
    public class LoseState : BaseState
    {
        public LoseState(StateContext context) : base(context) { }

        public override void Enter()
        {
            base.Enter();
            _stateContext.ScreenController.EnableFade(() =>
            {
                SwitchToGameplayState();
            });
        }

        private void SwitchToGameplayState()
        {
            _stateContext.StateSwitcher.SwitchState<GameplayState>();
        }

        public override void Exit()
        {
            base.Exit();
            _stateContext.ScreenController.DisableFade();
        }
    }
}
