using System;
using UnityEngine;

namespace Tetris.Controllers
{
    public class InputController : IInputController
    {
        //[Tooltip("Minimal time needed to detect key press")]
        [SerializeField] private float _pressTreshold = 0.5f;
        //[Tooltip("Interval between input reading")]
        [SerializeField] private float _inputInterval = 0.1f;
    
        private float _pressTime;
        private float _lastInputTime;
    
        public Action OnLeftPress;
        public Action OnRightPress;
        public Action OnDownPress;
        public Action OnUpPress;

        public InputController()
        {
            
        }
        
        public void Tick()
        {
            if(GetKey(KeyCode.LeftArrow))
                OnLeftPress?.Invoke();
            else if(GetKey(KeyCode.RightArrow))
                OnRightPress?.Invoke();
            else if(GetKey(KeyCode.DownArrow))
                OnDownPress?.Invoke();
            else if(Input.GetKeyDown(KeyCode.UpArrow))
                OnUpPress?.Invoke();
        }
    
        private bool GetKey(KeyCode key) 
        {
            bool isKeyDown = Input.GetKeyDown(key);
            bool isKeyPressed = 
                Input.GetKey(key) && 
                Time.time - _pressTime > _pressTreshold && 
                Time.time - _lastInputTime > _inputInterval;
        
            if (isKeyDown) 
                _lastInputTime = Time.time;
        
            if (isKeyPressed) 
                _lastInputTime = Time.time;
        
            return isKeyDown || isKeyPressed;
        }
    }
}
