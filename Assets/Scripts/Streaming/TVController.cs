using System;
using System.Collections.Generic;
using Leon;
using NaughtyAttributes;
using NUnit.Framework;
using UnityEngine;

namespace VideoStreaming
{
    public class TVController : SceneSingleton<TVController>
    {
        public static Func<List<StreamingTV>, List<StreamingTV>> OnAddToCollection;

        [BoxGroup("Streaming TV's"), SerializeField]
        private List<StreamingTV> _streamingTVList = new List<StreamingTV>();

        [BoxGroup("Hearing Distance"), SerializeField]
        private float _maxVolumeDistance = 5f;

        [BoxGroup("Hearing Distance"), SerializeField]
        private float _hearingDistance = 30f;

        private void Start()
        {
            _streamingTVList = OnAddToCollection(_streamingTVList);
            Assert.IsNotNull(PlayerPosition.Instance,
                $"Please attach the PlayerPosition.cs to the player gameobject. Video streams will not update their volume according to player distance.");
        }

        private void Update() => HandleTVVolume();

        private void HandleTVVolume()
        {
            for (int i = 0; i < _streamingTVList.Count; i++)
            {
                float newVolume = 0f;
                float distanceToPlayer = float.PositiveInfinity;
                Vector3 tvPosition = _streamingTVList[0].transform.position;
                
                if (PlayerPosition.Instance != null)
                {
                    Vector3 playerPosition = PlayerPosition.Instance.GetWorldPosition;
                    distanceToPlayer = Vector3.Distance(tvPosition, playerPosition);
                }

                distanceToPlayer = Mathf.Clamp(distanceToPlayer, 0f, _hearingDistance);
                if (distanceToPlayer <= _maxVolumeDistance)
                    newVolume = 1f;
                else
                    newVolume = 1 - distanceToPlayer / _hearingDistance;

                WebGLStreamController.Instance.SetNewVolume(_streamingTVList[0].StreamIndex, newVolume);
            }
        }
    }
}