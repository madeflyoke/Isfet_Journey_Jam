using System;
using UnityEngine;

namespace Mirror
{
    public class MirrorBeamTrigger : MonoBehaviour
    {
        public event Action OnPlayerCrossBeam;

        public void OnTriggerEnter(Collider other)
        {
            //Check on Player cross bream
            //OnPlayerCrossBeam?.Invoke();
        }
    }
}
