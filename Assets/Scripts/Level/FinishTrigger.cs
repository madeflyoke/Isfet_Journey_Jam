using System;
using UnityEngine;

namespace Level
{
    [RequireComponent(typeof(Collider))]
    public class FinishTrigger : MonoBehaviour
    {
        public event Action OnPlayerReachFinish;

        private void OnTriggerEnter(Collider other)
        {
            //if player then PlayerReachFinish?.Invoke();
        }
    }
}
