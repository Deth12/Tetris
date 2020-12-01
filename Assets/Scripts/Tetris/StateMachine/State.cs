namespace Tetris.StateMachine
{
    public abstract class State
    {
        protected StateMachine _stateMachine;
        protected GameController _gameController;
        protected State _nextState;

        protected State(StateMachine stateMachine, GameController gameController)
        {
            _stateMachine = stateMachine;
            _gameController = gameController;
        }

        public State NextState => _nextState;
        public void SetNextState(State state) => _nextState = state;
        
        public virtual void Enter(){}
        public virtual void HandleInput(){}
        public virtual void Exit() {}
        
        protected void SwitchToState(State newState)
        {
            if (newState == null) 
                return;
            
            _stateMachine.ChangeState(newState);
        }
    }
}