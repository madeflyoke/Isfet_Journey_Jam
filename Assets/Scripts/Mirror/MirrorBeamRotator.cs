using System;
using DG.Tweening;
using EasyButtons;
using UnityEngine;

namespace Mirror
{
    public class MirrorBeamRotator : MonoBehaviour
    {
        [SerializeField] private Transform _aboveBeamPivot;
        [SerializeField] private Transform _forwardBeamPivot;
        [SerializeField] private float _maxAngle;
        [SerializeField] private float _duration;
        private Vector3 _defaultForwardBeamEulers;
        private Vector3 _defaultAboveBeamEulers;
        
        private Sequence _rotationSequence;
        private float _aboveAngleCorrected;

        private void Start()
        {
            _aboveAngleCorrected = _maxAngle / 4f;
            SetStartPoses();
        }

        [Button]
        private void SetStartPoses()
        {
            _defaultAboveBeamEulers = _aboveBeamPivot.localEulerAngles;
            _aboveBeamPivot.localRotation = Quaternion.Euler(_defaultAboveBeamEulers.x, 
                _defaultAboveBeamEulers.y-_aboveAngleCorrected, _defaultAboveBeamEulers.z);
            
            _defaultForwardBeamEulers = _forwardBeamPivot.localEulerAngles;
            _forwardBeamPivot.localRotation = Quaternion.Euler(_defaultForwardBeamEulers.x, 
                _defaultForwardBeamEulers.y, _defaultForwardBeamEulers.z-_maxAngle);
        }

        [Button]
        private void StartRot()
        {
            _rotationSequence = DOTween.Sequence();
            _rotationSequence
                .Append(_forwardBeamPivot.DOLocalRotate(new Vector3(_defaultForwardBeamEulers.x,
                        _defaultForwardBeamEulers.y, _defaultForwardBeamEulers.z + _maxAngle), _duration)
                    .SetEase(Ease.InOutSine))
                .Join(_aboveBeamPivot.DOLocalRotate(new Vector3(_defaultAboveBeamEulers.x,
                        _defaultAboveBeamEulers.y + _aboveAngleCorrected, _defaultAboveBeamEulers.z), _duration)
                    .SetEase(Ease.InOutSine))
                .SetLoops(-1, LoopType.Yoyo);
        }
        
        private void OnDisable()
        {
            _rotationSequence?.Kill();
        }
    }
}
