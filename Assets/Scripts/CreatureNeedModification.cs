using System;
using UnityEngine;

namespace LD56.V2 {
	[Serializable]
	public class CreatureNeedModification {
		[SerializeField] protected CreatureNeed need;
		[SerializeField] protected AnimationCurve modificationOverTime;

		public CreatureNeed Need => need;
		public AnimationCurve ModificationOverTime => modificationOverTime;
	}
}