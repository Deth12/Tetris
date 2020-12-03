using System;
using Tetris.Configs;
using UnityEngine;

namespace Tetris.Controllers
{
    public class InputController : IInputController
    {
        private readonly InputConfig _inputConfig;

        private float _lastInputRegistered;
    
        public Action OnMoveLeftPress;
        public Action OnMoveRightPress;
        public Action OnRotatePress;
        public Action OnFallPress;

        public InputController(InputConfig inputConfig)
        {
            _inputConfig = inputConfig;
        }
        
        public void Tick()
        {
            if (GetKey(_inputConfig.MoveLeftKey))
                OnMoveLeftPress?.Invoke();
            else if(GetKey(_inputConfig.MoveRightKey))
                OnMoveRightPress?.Invoke();
            else if(GetKey(_inputConfig.FallKey))
                OnFallPress?.Invoke();
            else if(Input.GetKeyDown(_inputConfig.RotateKey))
                OnRotatePress?.Invoke();
        }
    
        private bool GetKey(KeyCode key)
        {
            bool isPressKeyRegistred =
                Input.GetKey(key) && Time.time - _lastInputRegistered > _inputConfig.InputInterval;

            if (isPressKeyRegistred)
                _lastInputRegistered = Time.time;

            return isPressKeyRegistred;
        }
    }
}
