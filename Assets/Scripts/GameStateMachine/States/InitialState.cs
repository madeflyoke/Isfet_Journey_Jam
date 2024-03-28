using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameStateMachine.States
{
    public class InitialState : BaseState
    {
        public InitialState(StateContext context) : base(context) { }

        public override void Enter()
        {
            SetupGame();
        }

        private async void SetupGame()
        {
            Application.targetFrameRate = 60;
            _stateContext.LevelLauncher.Init();
            _stateContext.ScreenController.OnTutorClosed += LaunchGame;
            await UniTask.Delay(2000);
            _stateContext.ScreenController.SetFadeHalf(() =>
            {
                _stateContext.ScreenController.ShowTutor();
            });
        }

        private void LaunchGame()
        {
            _stateContext.StateSwitcher.SwitchState<GameplayState>();
        }

        public override void Exit()
        {
            _stateContext.ScreenController.OnTutorClosed -= LaunchGame;
        }
    }
}
