using LD56.V2;
using LD56v2;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
	[SerializeField] protected Camera camera;
	[SerializeField] protected LayerMask interactionCastMask;

	private Controls Controls { get; set; }

	private void OnEnable() {
		Controls = new Controls();
		Controls.Player.Interact.performed += HandleInteractionPerformed;
		Controls.Player.Exit.performed += HandleExitPerformed;
		Controls.Enable();
	}

	private void OnDisable() {
		Controls.Player.Interact.performed -= HandleInteractionPerformed;
		Controls.Player.Exit.performed -= HandleExitPerformed;
	}

	private void HandleInteractionPerformed(InputAction.CallbackContext obj) {
		if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out var hitInfo, 2000, interactionCastMask)) {
			var interactable = hitInfo.collider.GetComponentInParent<IInteractable>();
			if (interactable != null && interactable.CanInteractNow()) {
				interactable.Interact();
			}
		}
	}

	private static void HandleExitPerformed(InputAction.CallbackContext callbackContext) {
		{
#if UNITY_EDITOR
			if (Application.isPlaying) {
				UnityEditor.EditorApplication.isPlaying = false;
			}
#endif
#if UNITY_WEBGL
			return
#endif
			Application.Quit();
		}
	}
}