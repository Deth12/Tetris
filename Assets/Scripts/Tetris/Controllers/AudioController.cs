using UnityEngine;
using Tetris.Managers;

namespace Tetris.Controllers
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioClip _musicTheme = default;

        private void Start()
        {
            AudioManager.Instance.SetMusic(_musicTheme);
        }
    }
}
