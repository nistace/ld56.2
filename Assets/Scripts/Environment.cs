using UnityEngine;

namespace LD56.V2 {
	public static class Environment {
		public static Vector3 MinPosition { get; set; }
		public static Vector3 MaxPosition { get; set; }

		public static Vector3 FixPosition(Vector3 position) => new Vector3(Mathf.Clamp(position.x, MinPosition.x, MaxPosition.x), Mathf.Clamp(position.y, MinPosition.y, MaxPosition.y),
			Mathf.Clamp(position.z, MinPosition.z, MaxPosition.z));
	}
}