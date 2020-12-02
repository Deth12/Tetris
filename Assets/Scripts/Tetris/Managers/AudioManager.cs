using System;
using UnityEngine;
using Tetris.Patterns;

namespace Tetris.Managers
{
    public class AudioManager : Singleton<AudioManager>
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.playOnAwake = false;
            _audioSource.loop = false;
        }

        public void PlayClip(AudioClip clip, float volume = 1f)
        {
            _audioSource.PlayOneShot(clip, volume);
        }

        public void SetMusic(AudioClip musicClip)
        {
            _audioSource.clip = musicClip;
            _audioSource.loop = true;
            _audioSource.Play();
        }
    }
}
