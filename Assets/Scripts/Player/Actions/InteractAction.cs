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
        public event Action<bool, string> OnCanInteractWithItem;

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
                if (SitAction.Instance.IsSitting)
                    SitAction.Instance.TryExecuteAction(null);
                else
                    ShootRaycast();

                _input.interact = false;
            }
        }

        private void CheckForInteractableItems()
        {
            var item = TryGetRaycastHitObject();
            string message = String.Empty;
            bool displayMessage = false;

            if (item.hasHitObject)
            {
                displayMessage = item.hasHitObject;
                IInteractableObject interactableObject = item.newHit.transform.GetComponent<IInteractableObject>();
                message = interactableObject.Message;
            }

            TryToShowInteractMessage(displayMessage, message);
        }

        private void TryToShowInteractMessage(bool displayMessage, string messageContent) =>
            OnCanInteractWithItem?.Invoke(displayMessage, messageContent);

        private void ShootRaycast()
        {
            if (SitAction.Instance.IsSitting) return;
            var item = TryGetRaycastHitObject();
            if (!item.hasHitObject) return;
            if (item.newHit.transform.TryGetComponent(out IInteractableObject interactableObject))
            {
                bool interacted = interactableObject.TryInteract();
                if (!interacted)
                    Debug.LogWarning($"The {interactableObject} was not interacted.");
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