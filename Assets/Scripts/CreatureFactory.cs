using UnityEngine;

namespace LD56.V2 {
	public class CreatureFactory : MonoBehaviour {
		[SerializeField] protected CreatureModel creaturePrefab;
		[SerializeField] protected Transform playerPosition;

		[SerializeField] protected float minColorValue = .4f;

		[ContextMenu("Spawn")]
		public void Spawn() {
			var instance = Instantiate(creaturePrefab, transform.position, Quaternion.Euler(0, Random.value * 360, 0), null);

			instance.GetComponent<CreatureController>().PlayerPosition = playerPosition;

			var red = Random.Range(minColorValue, 1);
			var green = Random.Range(minColorValue, 1);
			var blue = Random.Range(minColorValue, 1);

			instance.Initialize(new Color(red, green, blue));
		}
	}
}