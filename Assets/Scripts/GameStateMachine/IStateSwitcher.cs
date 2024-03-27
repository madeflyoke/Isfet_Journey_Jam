namespace GameStateMachine
{
    public interface IStateSwitcher<T> where T:BaseState
    {
        public void SwitchState<T>();
    }
}
