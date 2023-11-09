using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fusion.KCC
{
    public class _RotateRings : MonoBehaviour
    {
        [SerializeField] private Transform[] objects;
        [SerializeField] private float minRotationSpeed = 30.0f;
        [SerializeField] private float maxRotationSpeed = 60.0f;

        private Quaternion[] initialRotations;
        private bool[] isReversing;
        private float[] rotationSpeeds;

        void Start()
        {
            InitializeRotationSettings();
        }

        void InitializeRotationSettings()
        {
            initialRotations = new Quaternion[objects.Length];
            isReversing = new bool[objects.Length];
            rotationSpeeds = new float[objects.Length];

            for (int i = 0; i < objects.Length; i++)
            {
                initialRotations[i] = objects[i].localRotation;
                isReversing[i] = false;
                rotationSpeeds[i] = Random.Range(minRotationSpeed, maxRotationSpeed);
            }
        }

        void Update()
        {
            RotateObjects();
        }

        void RotateObjects()
        {
            for (int i = 0; i < objects.Length; i++)
            {
                Quaternion currentRotation = objects[i].localRotation;

                // Determine the rotation direction
                float direction = isReversing[i] ? -1.0f : 1.0f;

                // Rotate the object with random speed
                currentRotation *= Quaternion.Euler(0, 0, direction * rotationSpeeds[i] * Time.deltaTime);

                // Check if the rotation is within bounds
                if (Quaternion.Angle(currentRotation, initialRotations[i]) >= 180.0f)
                {
                    // Reverse the direction when the rotation exceeds 180 degrees
                    isReversing[i] = !isReversing[i];
                }

                // Apply the new rotation
                objects[i].localRotation = currentRotation;
            }
        }
    }
}