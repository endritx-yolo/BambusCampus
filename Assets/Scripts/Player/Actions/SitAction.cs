using System;
using System.Collections;
using Leon;
using StarterAssets;
using UnityEngine;

namespace PlayerActions
{
    public class SitAction : SceneSingleton<SitAction>
    {
        public event Action OnSit;
        public event Action OnStand;
        
        private Animator _animator;
        private OfficeChair _currentOfficeChair;
        private WaitForSeconds _wait;
        private Coroutine _standUpCoroutine;
        private Coroutine _standDownCoroutine;
        private CharacterController _characterController;

        private bool _isSitting;
        private bool _isStandingUp;

        private const string _sitOnOfficeChair = "SitOnOfficeChair";

        #region Properties

        public OfficeChair CurrentOfficeChair => _currentOfficeChair;
        public bool IsSitting => _isSitting;
        public bool IsStandingUp => _isStandingUp;

        #endregion

        private void Awake()
        {
            _wait = new WaitForSeconds(.25f);
            _animator = GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();
        }

        public bool TryExecuteAction(OfficeChair officeChair)
        {
            if (officeChair != null) _currentOfficeChair = officeChair;

            if (_standUpCoroutine != null)
            {
                StopCoroutine(_standUpCoroutine);
                _standUpCoroutine = null;
            }

            if (_standDownCoroutine != null)
                StopCoroutine(_standDownCoroutine);

            _isSitting = !_isSitting;
            
            if (_characterController.enabled)
                _characterController.enabled = false;
            
            transform.forward = _currentOfficeChair.FacingDirection;
            transform.position = _isSitting ? _currentOfficeChair.SitPosition : _currentOfficeChair.StandPosition;
            _animator.SetBool(_sitOnOfficeChair, _isSitting);
            
            if (!_isSitting)
            {
                _characterController.enabled = true;
                if (!IsStandingUp) _standUpCoroutine = StartCoroutine(StandUp());
                _currentOfficeChair = null;
                OnStand?.Invoke();
            }
            else
            {
                OnSit.Invoke();
            }

            return true;
        }
        
        private IEnumerator StandUp()
        {
            _isStandingUp = true;
            yield return _wait;
            _isStandingUp = false;
        }
    }
}