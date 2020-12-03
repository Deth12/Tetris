using System;
using System.Collections.Generic;
using Tetris.Scriptable;
using Tetris.Constants;
using UnityEngine;
using Tetris.Patterns;
using Zenject;

namespace Tetris.Managers
{
    public class AudioManager : Singleton<AudioManager>
    {
        private AudioLoader _audioLoader;
        private AudioSource _audioSource;
        
        private readonly Dictionary<string, AudioClip> _audioCache = new Dictionary<string, AudioClip>();

        private void Awake()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            
            _audioLoader = Resources.Load<AudioLoader>(ConstantPaths.AUDIO_LOADER);
            if(_audioLoader == null)
                throw new Exception("AudioLoader is not found");
        }

        private void Start()
        {
            PlayMusicByName(ConstantAudioNames.MUSIC_MAIN);
        }

        public void PlayClipByName(string clipName, bool cache = true, float volume = 1f)
        {
            if (!_audioCache.ContainsKey(clipName))
            {
                AudioClip clip = _audioLoader.LoadAudioClip(clipName);
                if (cache)
                {
                    _audioCache.Add(clipName, clip);
                }
                PlayClip(clip);
            }
            PlayClip(_audioCache[clipName]);
        }

        private void PlayClip(AudioClip clip, float volume = 1f)
        {
            _audioSource.PlayOneShot(clip, volume);
        }
        
        public void PlayMusicByName(string clipName, bool cache = true, float volume = 1f)
        {
            if (!_audioCache.ContainsKey(clipName))
            {
                AudioClip clip = _audioLoader.LoadAudioClip(clipName);
                if (cache)
                {
                    _audioCache.Add(clipName, clip);
                }
                PlayMusic(clip);
            }
            PlayMusic(_audioCache[clipName]);
        }

        private void PlayMusic(AudioClip clip, float volume = 1f)
        {
            _audioSource.clip = clip;
            _audioSource.loop = true;
            _audioSource.Play();
        }

        private void OnDestroy()
        {
            _audioCache.Clear();
            _audioLoader.UnloadAudioClips();
        }
    }
}
