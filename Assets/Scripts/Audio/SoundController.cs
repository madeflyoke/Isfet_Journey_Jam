using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Lean.Pool;
using UnityEngine;

namespace Main.Scripts.Audio
{
    public class SoundController : MonoBehaviour
    {
        public static SoundController Instance { get; private set; }
        public float SoundsVolume => _soundsVolume;
        
        [SerializeField] private AudioSource _audioSourcePrefab;
        [SerializeField, Range(0.01f,1f)] private float _soundsVolume;
        [SerializeField] private List<ClipBySoundType> _clips;
        [SerializeField] private AudioSource _mainMusicSource;
        [SerializeField] private List<AudioClip> _musicClips;
        private float _defaultMusicVolume;
        private CancellationTokenSource _cts;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                return;
            }
            Destroy(gameObject);
        }

        private void Start()
        {
            StartMusicPlayer();
        }

        private async void StartMusicPlayer()
        {
            _cts = new CancellationTokenSource();
            var index = 1;
            while (true)
            {
                _mainMusicSource.Stop();
                index = index == 0 ? 1 : 0;
                _mainMusicSource.clip = _musicClips[index];
                _mainMusicSource.Play();
                await UniTask.Delay(TimeSpan.FromSeconds(_mainMusicSource.clip.length+1f), cancellationToken: _cts.Token);
            }
        }
        

        private void OnDisable()
        {
            _cts?.Cancel();
        }

        public void PlayClip(SoundType soundType, float customVolume = 0f, float customPitch = 1f)
        {
            var audioSource = LeanPool.Spawn(_audioSourcePrefab);
            audioSource.clip = _clips.FirstOrDefault(x=>x.SoundType==soundType).Clip;
            audioSource.volume = customVolume!=0f? customVolume: _soundsVolume;
            audioSource.pitch = customPitch;
            LeanPool.Despawn(audioSource, audioSource.clip.length);
            audioSource.Play();
        }
        
        [Serializable]
        private struct ClipBySoundType
        {
            public SoundType SoundType;
            public AudioClip Clip;
        }
    }
    
}
