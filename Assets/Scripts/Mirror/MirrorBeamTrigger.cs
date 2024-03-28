using System;
using Character;
using UnityEngine;

namespace Mirror
{
    public class MirrorBeamTrigger : MonoBehaviour
    {
        public event Action OnPlayerCrossBeam;

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<MainCharacter>(out MainCharacter character))
            {
               character.OnDie();
               OnPlayerCrossBeam?.Invoke();
            }
        }
    }
}
