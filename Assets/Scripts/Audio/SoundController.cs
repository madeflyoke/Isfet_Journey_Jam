using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using EasyButtons;
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
        private int _musicIndex;
        private float _defaultMusicVolume;
        
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
            _defaultMusicVolume = _mainMusicSource.volume;

            SetNextMusic();
            
        }

        [Button]
        private void SetNextMusic()
        {
            _mainMusicSource.Stop();
            _mainMusicSource.clip = _musicClips[_musicIndex % _musicClips.Count];
            _musicIndex++;
            Invoke(nameof(SetNextMusic), _mainMusicSource.clip.length);
            _mainMusicSource.volume = 0f;
            _mainMusicSource.Play();
            _mainMusicSource.DOFade(_defaultMusicVolume, 10f);
        }

        private void OnDisable()
        {
            CancelInvoke();
        }

        public void PlayClip(SoundType soundType, float customVolume = 0f)
        {
            var audioSource = LeanPool.Spawn(_audioSourcePrefab);
            audioSource.clip = _clips.FirstOrDefault(x=>x.SoundType==soundType).Clip;
            audioSource.volume = customVolume!=0f? customVolume: _soundsVolume;
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
