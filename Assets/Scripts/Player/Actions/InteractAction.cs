using System.Collections;
using System.Collections.Generic;
using System;
using Leon;
using NaughtyAttributes;
using StarterAssets;
using UnityEngine;
using VideoStreaming;

namespace PlayerActions
{
    public class InteractAction : SceneSingleton<InteractAction>
    {
        public event Action OnInteract;
        public event Action<bool> OnCanInteractWithItem;

        private StarterAssetsInputs _input;

        [BoxGroup("Raycast"), SerializeField] private Transform _raycastOrigin;
        [BoxGroup("Raycast"), SerializeField] private float _raycastLength;
        [BoxGroup("Raycast"), SerializeField] private LayerMask _layerMask;

        [BoxGroup("Item Check Interval"), SerializeField]
        private float _itemCheckInterval = .5f;

        private float _tempItemCheckInterval;

        private void Awake()
        {
            _tempItemCheckInterval = _itemCheckInterval;
            _input = GetComponent<StarterAssetsInputs>();
        }


        private void Update()
        {
            if (_tempItemCheckInterval > 0f)
            {
                _tempItemCheckInterval -= Time.deltaTime;
                if (_tempItemCheckInterval <= 0f)
                {
                    CheckForInteractableItems();
                    _tempItemCheckInterval = _itemCheckInterval;
                }
            }

            if (_input.interact)
            {
                ShootRaycast();
                _input.interact = false;
            }
        }

        private void CheckForInteractableItems()
        {
            var item = TryGetRaycastHitObject();
            OnCanInteractWithItem?.Invoke(item.hasHitObject);
        }

        private void ShootRaycast()
        {
            var item = TryGetRaycastHitObject();
            if (!item.hasHitObject) return;
            if (item.newHit.transform.TryGetComponent(out StreamingTV streamingTV))
            {
                bool togglePlayPause = streamingTV.TryTogglePlayPause();
                if (!togglePlayPause)
                    Debug.LogWarning($"The {streamingTV} was not toggled.");
            }
            /*Debug.DrawRay(_raycastOrigin.position,
                _raycastOrigin.TransformDirection(Vector3.forward) * _raycastLength, Color.green);*/
        }

        private (RaycastHit newHit, bool hasHitObject) TryGetRaycastHitObject()
        {
            RaycastHit hit;

            if (Physics.Raycast(_raycastOrigin.position,
                    _raycastOrigin.TransformDirection(Vector3.forward),
                    out hit,
                    _raycastLength,
                    _layerMask))
                return (hit, true);
            return (hit, false);
        }
    }
}