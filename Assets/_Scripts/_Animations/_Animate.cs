using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fusion.KCC
{
    public class _Animate : MonoBehaviour
    {
        [SerializeField] private GameObject[] gameObjects;
        [SerializeField] private float minY = 1f;  // Minimum Y position
        [SerializeField] private float maxY = 5f;  // Maximum Y position
        [SerializeField] private float animationDuration = 2.0f;  // Duration of the animation in seconds

        private float animationTimer;
        private Vector3[] initialPositions;
        private Vector3[] targetPositions;
        private bool isAnimating;

        void Start()
        {
            InitializeAnimation();
        }

        void Update()
        {
            if (isAnimating)
            {
                animationTimer += Time.deltaTime;

                for (int i = 0; i < gameObjects.Length; i++)
                {
                    float t = Mathf.Clamp01(animationTimer / animationDuration);
                    gameObjects[i].transform.position = Vector3.Lerp(initialPositions[i], targetPositions[i], t);
                }

                if (animationTimer >= animationDuration)
                {
                    isAnimating = false;
                    InitializeAnimation();
                }
            }
        }

        void InitializeAnimation()
        {
            isAnimating = true;
            animationTimer = 0f;

            initialPositions = new Vector3[gameObjects.Length];
            targetPositions = new Vector3[gameObjects.Length];

            for (int i = 0; i < gameObjects.Length; i++)
            {
                initialPositions[i] = gameObjects[i].transform.position;
                targetPositions[i] = new Vector3(
                    initialPositions[i].x,
                    Random.Range(minY, maxY),
                    initialPositions[i].z
                );
            }
        }
    }
}
