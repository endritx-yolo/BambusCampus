using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using PlayerActions;
using TMPro;
using UnityEngine;

namespace PlayerInteraction
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _interactText;
        [BoxGroup("Color"), SerializeField] private Color _colorFadeIn;
        [BoxGroup("Color"), SerializeField] private Color _colorFadeOut;

        private InteractAction _interactAction;

        private bool _followCamera;

        private void Awake() => _interactAction = GetComponentInParent<InteractAction>();
        private void OnEnable() => _interactAction.OnCanInteractWithItem += InteractAction_OnCanInteractWithItem;
        private void OnDisable() => _interactAction.OnCanInteractWithItem -= InteractAction_OnCanInteractWithItem;

        private void Update()
        {
            if (!_followCamera) return;
            _interactText.transform.LookAt(Camera.main.transform);
        }

        private void InteractAction_OnCanInteractWithItem(bool displayInteractPopup)
        {
            _followCamera = displayInteractPopup;
            _interactText.color = displayInteractPopup ? _colorFadeIn : _colorFadeOut;
        }
    }
}