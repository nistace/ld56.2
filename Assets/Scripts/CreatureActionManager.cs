using UnityEngine;
using UnityEngine.Events;

namespace LD56.V2 {
	[RequireComponent(typeof(CreatureController))]
	public class CreatureActionManager : MonoBehaviour {
		private CreatureController creatureController { get; set; }
		private ICreatureAction[] actions { get; set; }
		private ICreatureAction CurrentAction { get; set; }

		public UnityEvent<string> OnAnimationRequested { get; } = new UnityEvent<string>();

		private void Start() {
			creatureController = GetComponent<CreatureController>();
			actions = GetComponentsInChildren<ICreatureAction>();
			foreach (var action in actions) {
				action.enabled = false;
			}
		}

		private void Update() {
			var bestActionScore = 0f;
			ICreatureAction bestAction = null;

			foreach (var action in actions) {
				var score = action.GetPickActionScore();
				if (score > bestActionScore) {
					bestAction = action;
					bestActionScore = score;
				}
			}

			if (bestAction != CurrentAction) {
				if (CurrentAction != null) CurrentAction.enabled = false;
				CurrentAction = bestAction;
				if (CurrentAction != null) CurrentAction.enabled = true;
			}

			creatureController.TargetPosition = CurrentAction?.GetTargetPosition(creatureController.CurrentPosition) ?? creatureController.CurrentPosition;
			creatureController.TargetAimPosition = CurrentAction?.GetAimTargetPosition(creatureController.CurrentPosition, creatureController.PlayerPosition.position)
																?? creatureController.PlayerPosition.position;
		}
	}
}