namespace Tetris.StateMachines
{
    public class StateMachine
    {
        public State CurrentState { get; private set; }

        public void Initialize(State startState)
        {
            CurrentState = startState;
            startState.Enter();
        }

        public void ChangeState(State newState)
        {
            CurrentState.Exit();
            EnterState(newState);
        }

        public void EnterState(State newState)
        {
            CurrentState = newState;
            newState.Enter();
        }
    }
}