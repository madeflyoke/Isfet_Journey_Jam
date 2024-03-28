using System;
using System.Collections.Generic;
using Character;
using UnityEngine;
using Zenject;

namespace Level
{
    public class LevelLauncher : MonoBehaviour
    {
        public event Action OnWin;
        [Inject] private MainCharacter _characterPrefab;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private FinishTrigger _finishTrigger;
        [SerializeField] private List<SavePoint> _savePoints = new List<SavePoint>();
        public MainCharacter Character { get; private set; }
        private SavePoint _lastSavePoint;
        private Vector3 spawnPosition => _startPoint.position;

        public void Init()
        {
            _savePoints.ForEach(sp=>sp.OnPlayerReachSavePoint+=UpdateLastSavePoint);
        }

        public void Launch()
        {
            SetupPlayer();
            SpawnPlayer();
        }

        public void DestroyPlayer()=>GameObject.Destroy(Character.gameObject);
        
        private void SetupPlayer()
        {
            Character = null;
            Character = Instantiate(_characterPrefab);
            Character.gameObject.SetActive(false);
        }

        private void UpdateLastSavePoint(SavePoint point)
        {
            if (_lastSavePoint == null)
            {
                _lastSavePoint = point;
                return;
            }
            var distFromLastPoint = Vector3.Distance(spawnPosition, _lastSavePoint.SpawnPosition);
            var distFromNewPoint = Vector3.Distance(spawnPosition, point.SpawnPosition);

            if (distFromLastPoint < distFromNewPoint)
                _lastSavePoint = point;
        }

        private void SpawnPlayer()
        {
            if (_lastSavePoint == null)
                Character.transform.position = _startPoint.position;
            else
                Character.transform.position = _lastSavePoint.SpawnPosition;
            
            Character.gameObject.SetActive(true);
        }
    }
}
