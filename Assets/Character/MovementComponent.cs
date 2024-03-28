using System;
using Main.Scripts.Audio;
using UnityEngine;

namespace Character
{
    public class MovementComponent : MonoBehaviour
    {
        [SerializeField] private CharacterController _controller;
        [Space]
        [SerializeField] public float _moveSpeed = 5f;        
        [SerializeField] public float _jumpHeight = 5f;        
        [SerializeField] public float _gravity = -9.8f;    
        [SerializeField] private float _jumpDistanceFactor = 2f;
        
        private Vector3 _moveDirection;
        private bool _isJumping;
        private bool _canMove = false;
        
        public void Initialize()
        {
            SetActive(true);
        }

        public void SetActive(bool isActive)
        {
            _canMove = isActive;
        }
        
        private void Update()
        {
            if (_canMove==false)
            {
                return;
            }
            
            if (_controller.isGrounded)
            {
                float x = Input.GetAxisRaw("Horizontal");
         
                _moveDirection = new Vector3(x*_moveSpeed, 0f, 0f);
                AdjustVelocityToGroundAngle();

                if (Input.GetButtonDown("Jump"))
                {
                    Jump();
                }
            }

            _moveDirection.y += _gravity * Time.deltaTime;

            _controller.Move(_moveDirection * Time.deltaTime);

            if (_isJumping)
            {
                if (_controller.isGrounded)
                {
                    _isJumping = false;
                    SoundController.Instance?.PlayClip(SoundType.JUMP, customVolume:0.05f, customPitch:0.9f);
                }
            }
        }

        
        private void OnControllerColliderHit(ControllerColliderHit _)
        {
            if (_isJumping)
            {
                _moveDirection = Vector3.zero; 
            }
        }

        private void AdjustVelocityToGroundAngle()
        {
            Ray ray = new Ray(transform.position, -transform.up);
            if (Physics.Raycast(ray, out RaycastHit hit, 1f))
            {
                var rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                var velocity = rotation * _moveDirection;
                if (velocity.y<0)
                {
                    _moveDirection = velocity;
                }
            }
        }

        private void Jump()
        {
            _isJumping = true;
            _moveDirection *= _jumpDistanceFactor;
            _moveDirection.y = _jumpHeight;
            SoundController.Instance?.PlayClip(SoundType.JUMP, customVolume:0.05f, customPitch:1.1f);
        }
        
#if UNITY_EDITOR
   
         private Vector3[] _prevPointsTrajectoryEditor;
      
         private void OnDrawGizmos()
         {
            if (_controller.isGrounded)
            {
               float horizontalInput = Input.GetAxisRaw("Horizontal");
   
               if (horizontalInput!=0)
               {
                  Vector3 startPoint = transform.position;
                  Vector3 jumpVector = new Vector3(horizontalInput * _jumpDistanceFactor * _jumpHeight, _jumpHeight, 0f);
   
                  _prevPointsTrajectoryEditor = CalculateTrajectoryPoints(startPoint, jumpVector, 10);
               }
            }
   
            if (_prevPointsTrajectoryEditor!=null)
            {
               DrawTrajectory(_prevPointsTrajectoryEditor);
            }
         }
      
         private Vector3[] CalculateTrajectoryPoints(Vector3 startPoint, Vector3 jumpVector, int numberOfPoints)
         {
            Vector3[] points = new Vector3[numberOfPoints];
   
            float timeIncrement = 1f / numberOfPoints;
            float time = 0f;
   
            for (int i = 0; i < numberOfPoints; i++)
            {
               Vector3 point = startPoint + jumpVector * time;
               point.y += _gravity * time * time * 0.5f;
   
               points[i] = point;
               time += timeIncrement;
            }
   
            return points;
         }
      
         private void DrawTrajectory(Vector3[] trajectoryPoints)
         {
            Gizmos.color = Color.red;
   
            for (int i = 1; i < trajectoryPoints.Length; i++)
            {
               Gizmos.DrawLine(trajectoryPoints[i - 1], trajectoryPoints[i]);
            }
         }
   
#endif     
    }
}
