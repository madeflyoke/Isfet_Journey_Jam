using System;
using Character;
using UnityEngine;

namespace Level
{
    [RequireComponent(typeof(Collider))]
    public class SavePoint : MonoBehaviour
    {
        public event Action<SavePoint> OnPlayerReachSavePoint;
        [SerializeField] private Transform _spawnPoint;
        public Vector3 SpawnPosition => _spawnPoint.position;
        public bool Active;
        private void OnTriggerEnter(Collider other)
        {
            if(!Active) return;
            if (other.TryGetComponent<MainCharacter>(out MainCharacter character))
            {
                OnPlayerReachSavePoint?.Invoke(this);
            }
        }
    }
}
