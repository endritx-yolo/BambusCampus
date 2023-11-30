using System;
using System.Collections;
using System.Collections.Generic;
using PlayerActions;
using TMPro;
using UnityEngine;

public class InstructionsPresenter : MonoBehaviour
{
    private MoviePanelTweeners _tweener;

    [SerializeField] private TextMeshProUGUI _instructionText;

    private void Awake()
    {
        _instructionText.text = String.Empty;
        _tweener = GetComponent<MoviePanelTweeners>();
    }

    private void OnEnable()
    {
        SitAction.Instance.OnSit += SitAction_OnSit;
        SitAction.Instance.OnStand += SitAction_OnStand;
    }

    private void OnDisable()
    {
        SitAction.Instance.OnSit -= SitAction_OnSit;
        SitAction.Instance.OnStand -= SitAction_OnStand;
    }

    private void SitAction_OnSit()
    {
        _tweener.ExecuteTween();
        _instructionText.gameObject.SetActive(true);
        _instructionText.text = SitAction.Instance.CurrentOfficeChair.StandMessage;
    }

    private void SitAction_OnStand()
    {
        _tweener.RevertTween();
        _instructionText.gameObject.SetActive(false);
        _instructionText.text = String.Empty;
    }

    private void Start() => _instructionText.text = String.Empty;
}