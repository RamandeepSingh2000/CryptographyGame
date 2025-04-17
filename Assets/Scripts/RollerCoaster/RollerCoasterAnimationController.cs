using Unity.Cinemachine;
using UnityEngine;

namespace RollerCoaster
{
    public class RollerCoasterAnimationController : MonoBehaviour
    {
        [SerializeField] private CinemachineSplineCart[] _splineCarts;
        [SerializeField] private float _speed = 1f;
        private float[] _initialOffsets;
        private float _currentProgress = 0f;

        [SerializeField] private bool _startMoving = false;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _initialOffsets = new float[_splineCarts.Length];
            for (int i = 0; i < _splineCarts.Length; i++)
            {
                _initialOffsets[i] = _splineCarts[i].SplinePosition;
            }
        }

        public void StartRide()
        {
            _startMoving = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (!_startMoving) return;

            // _currentProgress += Time.deltaTime * _speed;
            for (int i = 0; i < _splineCarts.Length; i++)
            {
                var currentPosition = _splineCarts[i].SplinePosition;
                _splineCarts[i].SplinePosition = currentPosition >= 1f ? 0f : currentPosition + Time.deltaTime * _speed;
            }
        }
    }
}