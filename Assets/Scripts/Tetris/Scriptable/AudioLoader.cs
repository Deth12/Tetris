using System.IO;
using UnityEngine;

namespace Tetris.Scriptable
{
    [CreateAssetMenu(menuName = "Tetris/AudioLoader", fileName = "AudioLoader")]
    public class AudioLoader: ScriptableObject
    {
        [Tooltip("Path to audio folder in resources")]
        [SerializeField] private string _audioResourcesPath;

        public AudioClip LoadAudioClip(string clipName)
        {
            return Resources.Load<AudioClip>(Path.Combine(_audioResourcesPath, clipName));
        }

        public void UnloadAudioClips()
        {
            Resources.UnloadUnusedAssets();
        }
    }
}

