using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LD56.V2 {
	public class CreatureNeedsManager : MonoBehaviour {
		[Serializable] protected class DefinedNeed {
			[SerializeField] protected CreatureNeed need;
			[SerializeField] protected float initialValue = 1;

			public CreatureNeed Need => need;
			public float InitialValue => initialValue;
		}

		[SerializeField] protected DefinedNeed[] definedNeeds;
		[SerializeField] protected CreatureNeedModification[] defaultNeedModifications;
		[SerializeField] protected bool debug;

		private Dictionary<CreatureNeed, float> NeedValues { get; } = new Dictionary<CreatureNeed, float>();
		private Dictionary<CreatureNeedModification, float> AdditionalModificationsSinceTime { get; } = new Dictionary<CreatureNeedModification, float>();

		private float BirthTime { get; set; }

		private void Start() {
			foreach (var definedNeed in definedNeeds) {
				NeedValues.TryAdd(definedNeed.Need, definedNeed.InitialValue);
			}
			BirthTime = Time.time;
		}

		private void Update() {
			foreach (var needModification in defaultNeedModifications) {
				Modify(needModification.Need, Time.deltaTime * needModification.ModificationOverTime.Evaluate(Time.time - BirthTime));
			}
			foreach (var needModification in AdditionalModificationsSinceTime) {
				Modify(needModification.Key.Need, Time.deltaTime * needModification.Key.ModificationOverTime.Evaluate(Time.time - needModification.Value));
			}
		}

		public void Add(IEnumerable<CreatureNeedModification> modifications) {
			foreach (var modification in modifications) {
				Add(modification);
			}
		}

		public void Add(CreatureNeedModification modification) {
			if (AdditionalModificationsSinceTime.ContainsKey(modification)) return;
			AdditionalModificationsSinceTime.Add(modification, Time.time);
		}
		
		public void Remove(IEnumerable<CreatureNeedModification> modifications) {
			foreach (var modification in modifications) {
				Remove(modification);
			}
		}

		public void Remove(CreatureNeedModification modification) {
			AdditionalModificationsSinceTime.Remove(modification);
		}

		public void Modify(CreatureNeed need, float delta) {
			if (NeedValues.ContainsKey(need)) {
				NeedValues[need] += Time.deltaTime * delta;
			}
		}

		public bool TryGetNeedValue(CreatureNeed need, out float needValue) => NeedValues.TryGetValue(need, out needValue);

		private void OnDrawGizmos() {
			if (!debug) return;
			Debug.Log(string.Join("    ", NeedValues.Select(t => $"{t.Key.name}: {t.Value:0000.000}")));
		}
	}
}