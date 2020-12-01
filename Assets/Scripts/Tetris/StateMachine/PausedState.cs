using UnityEngine;

namespace Tetris.StateMachine
{
    public class PausedState : State
    {
        
        public PausedState(StateMachine stateMachine, GameController gameController) : base(stateMachine, gameController)
        {
            
        }
        
        public override void Enter()
        {
            base.Enter();
            _gameController.PauseGame();
        }

        public override void HandleInput()
        {
            base.HandleInput();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_nextState == null) 
                    return;
                _stateMachine.ChangeState(_nextState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            _gameController.UnpauseGame();
        }
    }
}