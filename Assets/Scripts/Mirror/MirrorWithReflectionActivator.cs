using System;
using Character;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace Mirror
{
    public class MirrorWithReflectionActivator : MonoBehaviour
    {
        [Inject] private MainCharacter _character;

        [SerializeField] private GameObject _reflectionedMirror;

        private void Start()
        {
            _reflectionedMirror.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject==_character.gameObject)
            {
                _reflectionedMirror.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject==_character.gameObject)
            {
                _reflectionedMirror.SetActive(false);
            }
        }
    }
}
