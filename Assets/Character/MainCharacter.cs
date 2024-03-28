using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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

      public void Initialize()
      {
         _mainVisualParticle.Play();
         _movementComponent.Initialize();
         _controller.detectCollisions = true;
      }

      [Button]
      public async void OnDie()
      {
         _movementComponent.SetActive(false);
         _mainVisualParticle.Stop();
         _controller.detectCollisions = false;
         await UniTask.Delay(2000);
         OnLose?.Invoke();
      }
      
   }
}
