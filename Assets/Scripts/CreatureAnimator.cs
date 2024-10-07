using UnityEngine;

namespace LD56.V2 {
	[RequireComponent(typeof(Animator))]
	public class CreatureAnimator : MonoBehaviour {
		private Animator animator;
		private CreatureController creatureController;
		private static readonly int onGroundAnimParam = Animator.StringToHash("OnGround");
		private static readonly int yVelocityAnimParam = Animator.StringToHash("YVelocity");
		private static readonly int groundVelocityAnimParam = Animator.StringToHash("GroundVelocity");

		private void Start() {
			animator = GetComponent<Animator>();
			creatureController = GetComponentInParent<CreatureController>();
		}

		private void Update() {
			animator.SetBool(onGroundAnimParam, creatureController.IsOnGround);
			animator.SetFloat(yVelocityAnimParam, creatureController.YVelocity);
			animator.SetFloat(groundVelocityAnimParam, creatureController.GroundVelocity);
		}

		public void Play(string triggerName) => animator.SetTrigger(triggerName);

		public void State(string state, bool b) {
			if (!animator) return;
			animator.SetBool(state, b);
		}
	}
}