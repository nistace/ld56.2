using UnityEngine;

namespace LD56.V2 {
	public class CreatureModel : MonoBehaviour {
		[SerializeField] protected MeshRenderer[] skinRenderers;

		public void Initialize(Color color) {
			foreach (var skinRenderer in skinRenderers) {
				skinRenderer.material = new Material(skinRenderer.sharedMaterial) { color = color };
			}
		}
	}
}