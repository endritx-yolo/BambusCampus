using UnityEngine;
using InGameDebugging;
#if ENABLE_INPUT_SYSTEM
using PlayerActions;
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")] public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool interact;

		[Header("Movement Settings")] public bool analogMovement;

		[Header("Mouse Cursor Settings")] public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			if (SitAction.Instance.IsSitting || SitAction.Instance.IsStandingUp) return;
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if (cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			if (SitAction.Instance.IsSitting || SitAction.Instance.IsStandingUp) return;
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			if (SitAction.Instance.IsSitting || SitAction.Instance.IsStandingUp) return;
			SprintInput(value.isPressed);
		}

		public void OnInteract(InputValue value)
		{
			InteractInput(value.isPressed);
		}

		public void OnToggleDebug(InputValue value)
		{
			if (DebugController.Instance != null)
				DebugController.Instance.ToggleDebug();
		}

		public void OnReturn(InputValue value)
		{
			if (DebugController.Instance != null)
				DebugController.Instance.OnHitReturn();
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		}

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void InteractInput(bool newInteractState)
		{
			interact = newInteractState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}