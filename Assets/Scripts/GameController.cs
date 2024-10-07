using System.Collections;
using UnityEngine;

namespace LD56.V2 {
	public class GameController : MonoBehaviour {
		[SerializeField] protected int spawns = 2;

		private void Start() {
			FindObjectOfType<LevelBuilder>().Build();
			StartCoroutine(Spawn());
		}

		private IEnumerator Spawn() {
			var factory = FindObjectOfType<CreatureFactory>();
			for (var i = 0; i < spawns; ++i) {
				factory.Spawn();
				yield return new WaitForSeconds(1);
			}
		}
	}
}