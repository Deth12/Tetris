using UnityEngine;

namespace Tetris.StateMachine
{
    public class GameplayState : State
    {
        public GameplayState(StateMachine stateMachine, GameController gameController) : base(stateMachine, gameController)
        {
            
        }
        
        public override void Enter()
        {
            
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
            
        }
    }
}