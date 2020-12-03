using UnityEngine;
using Tetris.Controllers;

namespace Tetris.StateMachines
{
    public class EndgameState : State
    {
        
        public EndgameState(StateMachine stateMachine, GameController gameController) : base(stateMachine, gameController)
        {
            
        }
        
        public override void Enter()
        {
            base.Enter();
        }

        public override void HandleInput()
        {
            base.HandleInput();
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (_nextState == null) 
                    return;
                _gameController.RestartGame();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_nextState == null) 
                    return;
                _gameController.ExitGame();
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}