using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace VideoStreaming
{
    public class StreamingTV : StreamingObject, IInteractableObject
    {
        [BoxGroup("Display Message"), SerializeField] private string _displayMessage;

        #region Properties

        public String Message => _displayMessage;

        #endregion
        
        public Vector3 GetWorldPosition() => transform.position;

        private void OnEnable()
        {
            if (TVController.Instance != null)
                TVController.OnAddToCollection += TVHolder_OnAddToCollection;
        }

        private void OnDisable()
        {
            if (TVController.Instance != null)
                TVController.OnAddToCollection -= TVHolder_OnAddToCollection;
        }

        private List<StreamingTV> TVHolder_OnAddToCollection(List<StreamingTV> streamingTVList)
        {
            streamingTVList.Add(this);
            return streamingTVList;
        }

        public override bool TryTogglePlayPause()
        {
            if (!_canBePaused) return false;
            if (_isPlaying)
            {
                _isPlaying = false;
                WebGLStreamController.Instance.PauseStream(_streamIndex);
                return true;
            }
            else
            {
                _isPlaying = true;
                WebGLStreamController.Instance.PlayStream(_streamIndex);
                return true;
            }
        }

        public bool TryInteract() => TryTogglePlayPause();
    }
}