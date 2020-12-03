using UnityEngine;

namespace Tetris.Configs
{
    [CreateAssetMenu(menuName = "Tetris/Configs/InputConfig", fileName = "InputConfig")]
    public class InputConfig : ScriptableObject
    {
        [Tooltip("Used for moving block left")]
        [SerializeField] private KeyCode _moveLeftKey = KeyCode.LeftArrow;
        [Tooltip("Used for moving block right")]
        [SerializeField] private KeyCode _moveRightKey = KeyCode.RightArrow;
        [Tooltip("Used for rotating block")]
        [SerializeField] private KeyCode _rotateKey = KeyCode.UpArrow;
        [Tooltip("Used for moving block down faster")]
        [SerializeField] private KeyCode _fallKey = KeyCode.DownArrow;

        [Tooltip("Interval between input reading")]
        [SerializeField] private float _inputInterval = 0.1f;

        public KeyCode MoveLeftKey => _moveLeftKey;
        public KeyCode MoveRightKey => _moveRightKey;
        public KeyCode RotateKey => _rotateKey;
        public KeyCode FallKey => _fallKey;

        public float InputInterval => _inputInterval;
    } 
}
