using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fusion.KCC
{
    public class _ChartsAnimation : MonoBehaviour
    {
        [SerializeField] private GameObject[] chartObjects;
        [SerializeField] private float minScaleSpeed = 1.0f; // Increase this value for faster scaling
        [SerializeField] private float maxScaleSpeed = 4.0f; // Increase this value for faster scaling
        [SerializeField] private float scaleWaitTime = 0.5f; // Time to wait before changing scale direction

        private float[] currentScaleValues;
        private float[] targetScaleValues;
        private float[] scaleSpeeds;
        private float[] waitTimers;
        private int[] scaleDirections; // 1 for up, -1 for down

        void Start()
        {
            InitializeScalingSettings();
        }

        void InitializeScalingSettings()
        {
            currentScaleValues = new float[chartObjects.Length];
            targetScaleValues = new float[chartObjects.Length];
            scaleSpeeds = new float[chartObjects.Length];
            waitTimers = new float[chartObjects.Length];
            scaleDirections = new int[chartObjects.Length];

            for (int i = 0; i < chartObjects.Length; i++)
            {
                currentScaleValues[i] = 1.0f; // Initial Y scale
                targetScaleValues[i] = Random.Range(1.0f, 2.0f); // Assign random target scale
                scaleSpeeds[i] = Random.Range(minScaleSpeed, maxScaleSpeed); // Assign random scale speed
                waitTimers[i] = 0.0f;
                scaleDirections[i] = 1; // Start by scaling up
            }
        }

        void Update()
        {
            ScaleCharts();
        }

        void ScaleCharts()
        {
            for (int i = 0; i < chartObjects.Length; i++)
            {
                if (waitTimers[i] > 0)
                {
                    // Wait for the specified time
                    waitTimers[i] -= Time.deltaTime;
                }
                else
                {
                    // Use Lerp to smoothly interpolate the current scale to the target scale
                    currentScaleValues[i] = Mathf.Lerp(currentScaleValues[i], targetScaleValues[i], scaleSpeeds[i] * Time.deltaTime);
                    chartObjects[i].transform.localScale = new Vector3(1, currentScaleValues[i], 1);

                    // Check if the scale has reached the target scale
                    if (Mathf.Approximately(currentScaleValues[i], targetScaleValues[i]))
                    {
                        // Change direction by swapping target and current scale
                        targetScaleValues[i] = (targetScaleValues[i] == 1.0f) ? 2.0f : 1.0f;
                        waitTimers[i] = scaleWaitTime; // Wait before changing direction again
                    }
                }
            }
        }
    }
}
