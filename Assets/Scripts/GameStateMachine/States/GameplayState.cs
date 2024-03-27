namespace GameStateMachine.States
{
    public class GameplayState : BaseState
    {
        public GameplayState(StateContext context) : base(context) { }

        public override void Enter()
        {
            _stateContext.LevelLauncher.Launch();
            _stateContext.LevelLauncher.Character.OnLose += SwitchToLoseState;
            _stateContext.LevelLauncher.OnWin += SwitchToFinishState;
        }

        private void SwitchToLoseState()
        {
          _stateContext.StateSwitcher.SwitchState<LoseState>();
        }
        
        private void SwitchToFinishState()
        {
            _stateContext.StateSwitcher.SwitchState<FinishState>();
        }

        public override void Exit()
        {
            base.Exit();
            _stateContext.LevelLauncher.Character.OnLose -= SwitchToLoseState;
            _stateContext.LevelLauncher.OnWin -= SwitchToFinishState;
        }
    }
}