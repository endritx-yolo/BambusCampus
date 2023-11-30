using System;
using NaughtyAttributes;
using PlayerActions;
using UnityEngine;

public class OfficeChair : MonoBehaviour, IInteractableObject
{
    private BoxCollider _boxCollider;
    
    [SerializeField] private Transform _sitTransform;
    [SerializeField] private Transform _standTransform;
    
    [BoxGroup("Display Message"), SerializeField] private string _displayMessage;
    [BoxGroup("Display Message"), SerializeField] private string _standMessage;

    #region Properties

    public Vector3 SitPosition => _sitTransform.position;
    public Vector3 StandPosition => _standTransform.position;
    public Vector3 FacingDirection => _sitTransform.forward;
    public string Message => _displayMessage;
    public string StandMessage => _standMessage;
    public Collider BoxCollider => _boxCollider;

    #endregion

    private void Awake() => _boxCollider = GetComponent<BoxCollider>();

    public bool TryInteract() => SitAction.Instance.TryExecuteAction(this);
}