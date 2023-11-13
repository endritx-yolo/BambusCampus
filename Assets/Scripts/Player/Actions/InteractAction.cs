using System.Collections;
using System.Collections.Generic;
using System;
using Leon;
using NaughtyAttributes;
using StarterAssets;
using UnityEngine;

namespace PlayerActions
{
    public class InteractAction : SceneSingleton<InteractAction>
    {
        public event Action OnInteract;

        private StarterAssetsInputs _input;

        [BoxGroup("Raycast"), SerializeField] private Transform _raycastOrigin;
        [BoxGroup("Raycast"), SerializeField] private float _raycastLength;
        [BoxGroup("Raycast"), SerializeField] private LayerMask _layerMask;

        private void Awake() => _input = GetComponent<StarterAssetsInputs>();

        private void Update()
        {
            if (_input.interact)
            {
                _input.interact = false;
                OnInteract?.Invoke();
            }

            ShootRaycast();
        }

        private void ShootRaycast()
        {
            RaycastHit hit;

            if (Physics.Raycast(_raycastOrigin.position, _raycastOrigin.TransformDirection(Vector3.forward),
                    out hit,
                    _raycastLength,
                    _layerMask))
            {
                Debug.DrawRay(_raycastOrigin.position,
                    _raycastOrigin.TransformDirection(Vector3.forward) * _raycastLength, Color.green);
                Debug.LogWarning($"{hit.transform.gameObject.name}");
            }
            else
            {
                Debug.DrawRay(_raycastOrigin.position,
                    _raycastOrigin.TransformDirection(Vector3.forward) * _raycastLength, Color.red);
            }
        }
    }
}