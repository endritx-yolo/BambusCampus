using System;
using System.Collections;
using System.Collections.Generic;
using Leon;
using NaughtyAttributes;
using UnityEngine;

namespace VideoStreaming
{
    public class TVHolder : SceneSingleton<TVHolder>
    {
        public static Func<List<StreamingTV>, List<StreamingTV>> OnAddToCollection;

        [BoxGroup("Streaming TV's"), SerializeField]
        private List<StreamingTV> _streamingTVList = new List<StreamingTV>();

        [BoxGroup("Hearing Distance"), SerializeField]
        private float _maxVolumeDistance = 5f;

        [BoxGroup("Hearing Distance"), SerializeField]
        private float _hearingDistance = 40f;

        private void Start() => _streamingTVList = OnAddToCollection(_streamingTVList);

        private void Update() => HandleTVVolume();

        private void HandleTVVolume()
        {
            for (int i = 0; i < _streamingTVList.Count; i++)
            {
                float newVolume = 0f;
                Vector3 tvPosition = _streamingTVList[0].transform.position;
                Vector3 playerPosition = StarterAssets.ThirdPersonController.Instance.transform.position;
                float distanceToPlayer = Vector3.Distance(tvPosition, playerPosition);
                distanceToPlayer = Mathf.Clamp(distanceToPlayer, 0f, _hearingDistance);
                if (distanceToPlayer <= _maxVolumeDistance)
                    newVolume = 1f;
                else
                    newVolume = 1 - distanceToPlayer / 40f;

                WebGLStreamController.Instance.SetNewVolume(_streamingTVList[0].StreamIndex, newVolume);
            }
        }
    }
}