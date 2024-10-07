using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LD56.V2 {
	[RequireComponent(typeof(CreatureNeedsManager))]
	public class CreaturePoopManager : MonoBehaviour, ICreatureAction {
		private static Queue<CreaturePoop> PoopPool { get; } = new Queue<CreaturePoop>();

		[SerializeField] protected CreatureNeedsManager needsManager;
		[SerializeField] protected CreatureNeed poopNeed;
		[SerializeField] protected float needChangeOnPoopSpawn = -.1f;
		[SerializeField] protected CreatureActionScore score;
		[SerializeField] protected CreaturePoop poopPrefab;
		[SerializeField] protected float expulsionForce;
		[SerializeField] protected float minTimeBeforeNewSpawn = .05f;
		[SerializeField] protected float maxTimeBeforeNewSpawn = .1f;
		[SerializeField] protected Transform origin;

		private float NextSpawnMinTime { get; set; }

		private void Reset() {
			needsManager = GetComponent<CreatureNeedsManager>();
		}

		[RuntimeInitializeOnLoadMethod]
		private static void HandleSceneLoaded() => PoopPool.Clear();

		private void Spawn() {
			var instance = PoopPool.Count > 0 ? PoopPool.Dequeue() : Instantiate(poopPrefab);
			instance.gameObject.SetActive(true);
			instance.Velocity = Vector3.zero;
			instance.transform.position = origin.position;
			instance.Expel(expulsionForce * origin.forward);
			needsManager.Modify(poopNeed, needChangeOnPoopSpawn);
			NextSpawnMinTime = Time.time + Random.Range(minTimeBeforeNewSpawn, maxTimeBeforeNewSpawn);
		}

		private void OnEnable() {
			NextSpawnMinTime = Time.time + Random.Range(minTimeBeforeNewSpawn, maxTimeBeforeNewSpawn);
		}

		public static void Pool(CreaturePoop poop) {
			poop.gameObject.SetActive(false);
			PoopPool.Enqueue(poop);
		}

		public float GetPickActionScore() => needsManager.TryGetNeedValue(poopNeed, out var needValue) ? score.GetScore(needValue, enabled) : 0;
		public Vector3 GetTargetPosition(Vector3 fromPosition) => fromPosition;
		public Vector3 GetAimTargetPosition(Vector3 current, Vector3 player) => player;

		private void Update() {
			if (Time.time < NextSpawnMinTime) return;
			Spawn();
		}
	}
}