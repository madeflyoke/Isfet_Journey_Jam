using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    public class LevelLauncher : MonoBehaviour
    {
        [SerializeField] private Transform _startPoint;
        [SerializeField] private FinishTrigger _finishTrigger;
        [SerializeField] private List<SavePoint> _savePoints = new List<SavePoint>();
        private GameObject Player;
        private SavePoint _lastSavePoint;
        private Vector3 spawnPosition => _startPoint.position;

        public void Init(GameObject player)
        {
            Player = player;
            _savePoints.ForEach(sp=>sp.OnPlayerReachSavePoint+=UpdateLastSavePoint);
        }

        public void Launch()
        {
            //player start
            SpawnPlayer();
            
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
                Player.transform.position = _startPoint.position;
            else
                Player.transform.position = _lastSavePoint.SpawnPosition;
        }
    }
}
