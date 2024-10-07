using UnityEngine;

namespace LD56.V2 {
	public class CreatureController : MonoBehaviour {
		[SerializeField] protected new Rigidbody rigidbody;
		[SerializeField] protected GroundChecker groundChecker;
		[SerializeField] protected float movementSpeed = 1;
		[SerializeField] protected float acceleration = 1;
		[SerializeField] protected float rotationSpeed = 5;
		[SerializeField] protected Transform playerPosition;
		[SerializeField] protected float distanceToTarget = .1f;

		private Vector3 Movement { get; set; }
		public float YVelocity => rigidbody.isKinematic ? Movement.y : rigidbody.velocity.y;
		public float GroundVelocity => (rigidbody.isKinematic ? new Vector3(Movement.x, 0, Movement.z).magnitude : new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z).magnitude) / movementSpeed;
		public bool IsOnGround => groundChecker.IsOnGround;
		public Vector3 Velocity => rigidbody.velocity;
		public bool ArmMovementAllowed { get; set; } = true;
		public Vector3 TargetPosition { get; set; }
		public Vector3 TargetAimPosition { get; set; }

		public Transform PlayerPosition {
			get => playerPosition;
			set => playerPosition = value;
		}

		public Vector3 CurrentPosition => transform.position;

		private void Start() {
			TargetPosition = transform.position;
		}

		private void FixedUpdate() {
			rigidbody.isKinematic = groundChecker.IsOnGround;
			transform.position = Environment.FixPosition(transform.position);
			if (!groundChecker.IsOnGround) return;
			if ((transform.position - TargetPosition).sqrMagnitude > distanceToTarget * distanceToTarget) {
				RotateTowards(TargetPosition);
				MoveTowards(TargetPosition);
			}
			else {
				MoveTowards(null);
				RotateTowards(TargetAimPosition);
			}
		}

		private void MoveTowards(Vector3? targetPosition) {
			var expectedMovementVector = targetPosition == null ? Vector3.zero : Vector3.ClampMagnitude(targetPosition.Value - transform.position, movementSpeed);
			Movement = Vector3.Slerp(Movement, expectedMovementVector, acceleration * Time.deltaTime);
			var actualMovement = new Vector3(Movement.x, 0, Movement.z);
			transform.position += actualMovement * Time.deltaTime;
		}

		private void RotateTowards(Vector3 targetPosition) {
			var aimTarget = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
			transform.forward = Vector3.RotateTowards(transform.forward, aimTarget - transform.position, Mathf.Deg2Rad * rotationSpeed * Time.deltaTime, 0);
		}

		private void OnDrawGizmos() {
			Gizmos.color = Color.red;
			Gizmos.DrawLine(transform.position, transform.position + rigidbody.velocity);
		}
	}
}