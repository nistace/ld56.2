using UnityEngine;
using UnityEngine.Events;

namespace LD56.V2 {
	public interface ICreatureAction {
		float GetPickActionScore();
		Vector3 GetTargetPosition(Vector3 fromPosition);
		Vector3 GetAimTargetPosition(Vector3 current, Vector3 player);
		bool enabled { get; set; }

	}
}