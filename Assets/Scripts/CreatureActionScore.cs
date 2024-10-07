using System;
using UnityEngine;

namespace LD56.V2 {
	[Serializable]
	public class CreatureActionScore {
		[SerializeField] protected AnimationCurve scoreWhenEnabled = AnimationCurve.Linear(0, 0, 1, 1);
		[SerializeField] protected AnimationCurve scoreWhenDisabled = AnimationCurve.Linear(0, 0, 1, 1);

		public float GetScore(float needValue, bool enabled) {
			var scoreCurve = enabled ? scoreWhenEnabled : scoreWhenDisabled;
			return scoreCurve.Evaluate(needValue);
		}
	}
}