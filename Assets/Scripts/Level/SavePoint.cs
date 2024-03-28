using System;
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
            //if player then OnPlayerReachSavePoint?.Invoke();
        }
    }
}
