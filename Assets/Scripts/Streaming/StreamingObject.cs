using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VideoStreaming
{
    public abstract class StreamingObject : MonoBehaviour
    {
        protected bool _isPlaying = true;
        [SerializeField] protected bool _canBePaused;
        [SerializeField] protected int _streamIndex;
        
        #region Properties
        
        public bool IsPlaying
        {
            get => _isPlaying;
            set => _isPlaying = value;
        }

        public bool CanBePaused
        {
            get => _canBePaused;
            set => _canBePaused = value;
        }

        public int StreamIndex
        {
            get => _streamIndex;
            set => _streamIndex = value;
        }
        
        #endregion

        public abstract bool TryTogglePlayPause();
    }
}