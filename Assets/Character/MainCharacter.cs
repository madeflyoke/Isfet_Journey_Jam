using System;
using UnityEngine;

namespace Character
{
   public class MainCharacter : MonoBehaviour
   {
      [SerializeField] private MovementComponent _movementComponent;

      private void Start()
      {
         _movementComponent.Initialize();
      }
      
   }
}
