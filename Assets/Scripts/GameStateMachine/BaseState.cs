namespace GameStateMachine
{
    public abstract class BaseState
    {
        protected StateContext _stateContext;
        protected BaseState(StateContext context) => _stateContext = context;
        public void Enter()
        {
            
        }
        
        public void Exit()
        {
            
        }
    }

    public class StateContext
    {
        public IStateSwitcher<BaseState> _StateSwitcher;
        // Input
        // LevelLauncher
        //UI Screens
    }
}
