using UnityEngine;

namespace LD56.V2 {
	public class CreatureSocialManager : MonoBehaviour, ICreatureAction, IInteractable {
		[SerializeField] protected CreatureAnimator animator;
		[SerializeField] protected CreatureNeedsManager needsManager;
		[SerializeField] protected CreatureNeed socialNeed;
		[SerializeField] protected CreatureActionScore score;
		[SerializeField] protected float interactionModification;
		[SerializeField] protected float exitStateAfterInteractionTime = 1;

		private float NextLeaveTime { get; set; }

		private void OnDisable() {
			animator.State("NeedSocial", false);
		}

		private void OnEnable() {
			animator.State("NeedSocial", true);
		}

		public float GetPickActionScore() => Time.time < NextLeaveTime ? 1 : needsManager.TryGetNeedValue(socialNeed, out var needValue) ? score.GetScore(needValue, enabled) : 0;
		public Vector3 GetTargetPosition(Vector3 fromPosition) => fromPosition;
		public Vector3 GetAimTargetPosition(Vector3 current, Vector3 player) => player;

		public bool CanInteractNow() => true;

		public void Interact() {
			needsManager.Modify(socialNeed, interactionModification);
			animator.Play("Scratched");
			NextLeaveTime = Time.time + exitStateAfterInteractionTime;
		}
	}
}