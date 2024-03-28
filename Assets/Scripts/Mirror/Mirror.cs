using System;
using UnityEngine;

namespace Mirror
{
    public class Mirror : MonoBehaviour
    {
        [SerializeField] private Transform _directionPoint;
        [SerializeField] private ParticleSystem _lightBeam;
        [SerializeField] private MirrorBeamTrigger _beamTrigger;
        [SerializeField] private LayerMask _mask =  ~0;
        private RaycastHit[] m_Results = new RaycastHit[5];
        private Vector3 _direction;
        public bool enable;

        private void OnEnable()
        {
            var heading = _directionPoint.transform.position - transform.position;
            _direction = heading / heading.magnitude; 
            
            //Setup particles direction
        }

        private void PlayBeamEffect()
        {
            
        }
        
        private void PlayGlowEffect()
        {
            
        }

        private void FixedUpdate()
        {
            // If we check player by Raycast
            if(!enable) return;
            int hits = Physics.RaycastNonAlloc(transform.position, _direction, m_Results, Mathf.Infinity, _mask);
            for (int i = 0; i < 5; i++)
            {
            }
        }

        private void OnDrawGizmos()
        {
            if(_directionPoint==null) return;
            
            Gizmos.color = Color.green;
            
            var heading = _directionPoint.transform.position - transform.position;
            var direction = heading / heading.magnitude;
            //Gizmos.DrawLine(transform.position, _directionPoint.transform.position);
            Gizmos.DrawRay(transform.position, direction *2);
        }
    }
}
