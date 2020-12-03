using UnityEngine;
using Tetris.Controllers;

namespace Tetris.StateMachines
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
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (_nextState == null) 
                    return;
                _gameController.RestartGame();
            }
        }

        public override void Exit()
        {
            base.Exit();
            _gameController.UnpauseGame();
        }
    }
}