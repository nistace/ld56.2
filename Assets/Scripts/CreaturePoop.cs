using UnityEngine;

namespace LD56.V2 {
	public class CreaturePoop : MonoBehaviour, IInteractable {
		[SerializeField] protected Rigidbody rigidbody;

		public Vector3 Velocity {
			get => rigidbody.velocity;
			set => rigidbody.velocity = value;
		}

		public void Expel(Vector3 force) => rigidbody.AddForce(force);

		private void Update() {
			if (transform.position.y > -50) return;

			CreaturePoopManager.Pool(this);
		}

		public bool CanInteractNow() => true;

		public void Interact() => CreaturePoopManager.Pool(this);
	}
}