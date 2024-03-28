using System;
using Character;
using Level;
using UI.Scripts;

namespace GameStateMachine
{
    public abstract class BaseState
    {
        protected StateContext _stateContext;
        protected BaseState(StateContext context) => _stateContext = context;
        public virtual void Enter() { }
        
        public virtual void Exit() { }
    }

    [Serializable]
    public class StateContext
    {
        public IStateSwitcher StateSwitcher;
        public LevelLauncher LevelLauncher;
        public ScreenController ScreenController;
    }
}
