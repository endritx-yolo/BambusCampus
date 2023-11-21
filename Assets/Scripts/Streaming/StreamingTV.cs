using System;
using System.Collections.Generic;
using UnityEngine;

namespace VideoStreaming
{
    public class StreamingTV : StreamingObject
    {
        public Vector3 GetWorldPosition() => transform.position;

        private void OnEnable()
        {
            TVHolder.OnAddToCollection += TVHolder_OnAddToCollection;
        }

        private void OnDisable()
        {
            TVHolder.OnAddToCollection -= TVHolder_OnAddToCollection;
        }

        private List<StreamingTV> TVHolder_OnAddToCollection(List<StreamingTV> streamingTVList)
        {
            streamingTVList.Add(this);
            Debug.LogWarning(this);
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
    }
}