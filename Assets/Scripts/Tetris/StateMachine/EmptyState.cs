using UnityEngine;
using Tetris.Controllers;

namespace Tetris.StateMachines
{
    public class PrepareState : State
    {
        public PrepareState(StateMachine stateMachine, GameController gameController) : base(stateMachine, gameController)
        {
            
        }

        public override void Enter()
        {
            
        }
        
        public override void HandleInput()
        {
            base.HandleInput();
            if (Input.anyKeyDown)
            {
                if (_nextState == null) 
                    return;
                _gameController.StartGame();
                _stateMachine.ChangeState(_nextState);
            }
        }

        public override void Exit()
        {
            
        }
    }
}