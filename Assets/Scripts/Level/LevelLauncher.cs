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
        [Inject] private MainCharacter _character;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private FinishTrigger _finishTrigger;
        [SerializeField] private List<SavePoint> _savePoints = new List<SavePoint>();
        public MainCharacter Character => _character;
        private SavePoint _lastSavePoint;
        private Vector3 spawnPosition => _startPoint.position;

        public void Init()
        {
            _savePoints.ForEach(sp=>
            {
                sp.OnPlayerReachSavePoint += UpdateLastSavePoint;
                sp.Active = true;
            });
            _finishTrigger.OnPlayerReachFinish += FinishGame;
        }

        public void Launch()
        {
            SetupPlayer();
            SpawnPlayer();
        }

        private void SetupPlayer()
        {
            _character.Initialize();
            _character.SetActive(false);
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
                _character.transform.position = _startPoint.position;
            else
                _character.transform.position = _lastSavePoint.SpawnPosition;
            
            _character.SetActive(true);
        }

        private void FinishGame()
        {
            _finishTrigger.OnPlayerReachFinish -= FinishGame;
            _savePoints.ForEach(sp=>sp.OnPlayerReachSavePoint-=UpdateLastSavePoint);
            Debug.Log("Finish");
        }
    }
}
