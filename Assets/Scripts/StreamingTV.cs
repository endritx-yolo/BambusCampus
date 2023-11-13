using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using PlayerActions;
using UnityEngine;

namespace VideoStreaming
{
    public class StreamingTV : MonoBehaviour
    {
        private bool _isPlaying = true;
        private bool _waitingForInput;
        [SerializeField] private int _streamIndex;

        private void OnEnable() => InteractAction.Instance.OnInteract += InteractAction_OnInteract;
        private void OnDisable() => InteractAction.Instance.OnInteract -= InteractAction_OnInteract;

        private void InteractAction_OnInteract()
        {
            if (!_waitingForInput) return;
            TogglePlayPause();
        }
        
        public void OnTriggerEnter(Collider other)
        {
            Debug.LogWarning("On Trigger Enter");
            if (!other.TryGetComponent(out CharacterController _)) return;
            _waitingForInput = true;
        }

        public void OnTriggerExit(Collider other)
        {
            Debug.LogWarning("On Trigger Exit");
            if (!other.TryGetComponent(out CharacterController _)) return;
            _waitingForInput = false;
        }

        [Button("TogglePlayPause")]
        private void TogglePlayPause()
        {
            //if (!_waitingForInput) return;
            if (_isPlaying)
            {
                _isPlaying = false;
                WebGLStreamController.Instance.PauseStream(_streamIndex);
            }
            else
            {
                _isPlaying = true;
                WebGLStreamController.Instance.PlayStream(_streamIndex);
            }
        }
    }
}