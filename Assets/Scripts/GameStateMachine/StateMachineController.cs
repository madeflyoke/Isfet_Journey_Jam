using System;
using System.Collections.Generic;
using GameStateMachine.States;
using UnityEngine;

namespace GameStateMachine
{
    public class StateMachineController : MonoBehaviour, IStateSwitcher<BaseState>
    {
        private Dictionary<Type, BaseState> _states;
        private BaseState _currentState;
        private StateContext _context;
        
        private void LoadStates()
        {
            _states = new Dictionary<Type, BaseState>()
            {
                {typeof(InitialState), new InitialState(_context)},
                {typeof(GameplayState), new GameplayState(_context)},
                {typeof(LoseState), new LoseState(_context)},
                {typeof(FinishState), new FinishState(_context)},
            };
        }

        private void BuildStateContext()
        {
            _context = new StateContext();
            _context._StateSwitcher = this;
        }

        public void SwitchState<T>()
        {
            Debug.Log(typeof(T));
            if (!_states.ContainsKey(typeof(T))) return;

            var nextState = _states[typeof(T)];

            if (_currentState != nextState)
            {
                if(_currentState!=null)
                    _currentState.Exit();

                _currentState = nextState;
                _currentState.Enter();
            }
        }
    }
}
