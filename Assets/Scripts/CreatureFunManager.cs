using System.Linq;
using UnityEngine;

namespace LD56.V2 {
	public class CreatureFunManager : MonoBehaviour, ICreatureAction {
		[SerializeField] protected CreatureAnimator animator;
		[SerializeField] protected CreatureNeedsManager needsManager;
		[SerializeField] protected CreatureNeed poopNeed;
		[SerializeField] protected CreatureActionScore score;
		[SerializeField] protected CreatureNeedModification[] creatureNeedModifications;
		[SerializeField] protected float forwardForce = 500;
		[SerializeField] protected Vector3 additionalForce = new Vector3(0, 200, 0);
		[SerializeField] protected float timeBetweenTwoShots = 1;
		[SerializeField] protected Transform shootOrigin;
		[SerializeField] protected string happyAnimationTriggerParameter = "Happy";
		[SerializeField] protected float happyProbability = .6f;

		private BallController targetBall { get; set; }
		private float NextShotTime { get; set; }

		private void OnDisable() {
			needsManager.Remove(creatureNeedModifications);
		}

		public float GetPickActionScore() => needsManager.TryGetNeedValue(poopNeed, out var needValue) ? score.GetScore(needValue, enabled) : 0;
		public Vector3 GetTargetPosition(Vector3 fromPosition) => targetBall && !targetBall.IsCloseEnough(fromPosition) && Time.time > NextShotTime ? targetBall.transform.position : fromPosition;

		public Vector3 GetAimTargetPosition(Vector3 current, Vector3 player) {
			if (!targetBall) return player;
			if (Time.time > NextShotTime) return player;
			if (!targetBall.IsCloseEnough(current) && (new Vector3(targetBall.transform.position.x, current.y, targetBall.transform.position.z) - current).sqrMagnitude < 1) {
				return player;
			}
			return targetBall.transform.position;
		}

		private void Update() {
			if (!targetBall) {
				targetBall = BallController.Balls.First();
			}
			if (targetBall) {
				needsManager.Add(creatureNeedModifications);
			}
			else {
				needsManager.Remove(creatureNeedModifications);
				return;
			}

			if (targetBall.IsCloseEnough(transform.position) && Time.time > NextShotTime) {
				targetBall.Shoot((targetBall.transform.position - shootOrigin.position).normalized * forwardForce + additionalForce);
				if (happyProbability > Random.value) animator.Play(happyAnimationTriggerParameter);
				NextShotTime = Time.time + timeBetweenTwoShots;
			}
		}
	}
}