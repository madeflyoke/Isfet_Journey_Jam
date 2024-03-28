using System;
using Character;
using UnityEngine;

namespace Level
{
    [RequireComponent(typeof(Collider))]
    public class FinishTrigger : MonoBehaviour
    {
        public event Action OnPlayerReachFinish;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<MainCharacter>(out MainCharacter character))
            {
                OnPlayerReachFinish?.Invoke();
            }
        }
    }
}
