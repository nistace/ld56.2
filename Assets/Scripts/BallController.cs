using System.Collections.Generic;
using LD56.V2;
using UnityEngine;

public class BallController : MonoBehaviour, IInteractable {
	public static HashSet<BallController> Balls { get; } = new HashSet<BallController>();
	[SerializeField] protected new Rigidbody rigidbody;
	[SerializeField] protected float distanceToHit = 1.2f;

	private void OnEnable() {
		Balls.Add(this);
	}

	private void OnDisable() {
		Balls.Remove(this);
	}

	public void Shoot(Vector3 force) {
		rigidbody.AddForce(force);
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, distanceToHit);
	}

	public bool IsCloseEnough(Vector3 fromPosition) => (fromPosition - transform.position).sqrMagnitude < distanceToHit * distanceToHit;
	public bool CanInteractNow() => true;
	public void Interact() => Shoot(Vector3.up * 3000);
}