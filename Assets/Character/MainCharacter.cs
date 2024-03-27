using System;
using EasyButtons;
using UnityEngine;

namespace Character
{
   public class MainCharacter : MonoBehaviour
   {
      [SerializeField] private MovementComponent _movementComponent;
      [SerializeField] private CharacterController _controller;
      [SerializeField] private GameObject _visualHolder;
      [SerializeField] private ParticleSystem _deathParticle;

      private void Start()
      {
         Initialize();
      }

      public void Initialize()
      {
         _movementComponent.Initialize();
         _controller.detectCollisions = true;
         _visualHolder.SetActive(true);
      }

      [Button]
      public void OnDie()
      {
         _movementComponent.SetActive(false);
         _visualHolder.SetActive(false);
         _controller.detectCollisions = false;
         _deathParticle.Play();
      }
      
   }
}
