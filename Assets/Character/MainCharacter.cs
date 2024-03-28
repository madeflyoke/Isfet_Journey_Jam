using System;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;

namespace Character
{
   public class MainCharacter : MonoBehaviour
   {
      public event Action OnLose;
      
      [SerializeField] private MovementComponent _movementComponent;
      [SerializeField] private CharacterController _controller;
      [SerializeField] private ParticleSystem _mainVisualParticle;

      private void Start()
      {
         Initialize();
      }

      public void Initialize()
      {
         _mainVisualParticle.Play();
         _movementComponent.Initialize();
         _controller.detectCollisions = true;
      }

      [Button]
      public void OnDie()
      {
         _movementComponent.SetActive(false);
         _mainVisualParticle.Stop();
         _controller.detectCollisions = false;
         
         OnLose?.Invoke();
      }
      
   }
}
