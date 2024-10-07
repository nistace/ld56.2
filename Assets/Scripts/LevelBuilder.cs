using System;
using UnityEngine;

namespace LD56.V2 {
	public class LevelBuilder : MonoBehaviour {
		[SerializeField] private new Camera camera;
		[SerializeField] protected GameObject[] walls;
		[SerializeField] protected Transform wallsParent;
		[SerializeField] private float depth = .5f;
		[SerializeField] private float wallThickness = .5f;
		[SerializeField] private float border = .1f;
		[SerializeField] private float distance = .1f;
		[SerializeField] private float planeDistance = 100;

		public float Width { get; private set; }
		public float Height { get; private set; }
		public float Depth => depth;

		[ContextMenu("Build")]
		public void Build() {
			var bottomLeftLocal = camera.transform.InverseTransformPoint(camera.ViewportToWorldPoint(new Vector3(0, 0, planeDistance)));
			var bottomRightLocal = camera.transform.InverseTransformPoint(camera.ViewportToWorldPoint(new Vector3(1, 0, planeDistance)));
			var topLeftLocal = camera.transform.InverseTransformPoint(camera.ViewportToWorldPoint(new Vector3(0, 1, planeDistance)));
			var topRightLocal = camera.transform.InverseTransformPoint(camera.ViewportToWorldPoint(new Vector3(1, 1, planeDistance)));

			Width = Mathf.Abs(bottomRightLocal.x - bottomLeftLocal.x);
			Height = Mathf.Abs(topLeftLocal.y - bottomLeftLocal.y);

			walls[0].transform.SetParent(camera.transform);
			walls[0].transform.localPosition = (bottomLeftLocal + bottomRightLocal) * .5f + Vector3.down * (wallThickness * .5f - border) + Vector3.forward * (depth * .5f + distance);
			walls[0].transform.localScale = new Vector3(Width + wallThickness * 2, wallThickness, depth);

			walls[1].transform.SetParent(camera.transform);
			walls[1].transform.localPosition = (topLeftLocal + topRightLocal) * .5f + Vector3.up * (wallThickness * .5f - border) + Vector3.forward * (depth * .5f + distance);
			walls[1].transform.localScale = new Vector3(Width + wallThickness * 2, wallThickness, depth);

			walls[2].transform.SetParent(camera.transform);
			walls[2].transform.localPosition = (bottomLeftLocal + topLeftLocal) * .5f + Vector3.left * (wallThickness * .5f - border) + Vector3.forward * (depth * .5f + distance);
			walls[2].transform.localScale = new Vector3(wallThickness, Height - border * 2, depth);

			walls[3].transform.SetParent(camera.transform);
			walls[3].transform.localPosition = (bottomRightLocal + topRightLocal) * .5f + Vector3.right * (wallThickness * .5f - border) + Vector3.forward * (depth * .5f + distance);
			walls[3].transform.localScale = new Vector3(wallThickness, Height - border * 2, depth);

			walls[4].transform.SetParent(camera.transform);
			walls[4].transform.localPosition = (bottomLeftLocal + topRightLocal) * .5f + Vector3.forward * (depth + distance + wallThickness * .5f);
			walls[4].transform.localScale = new Vector3(Width + (wallThickness - border) * 2, Height + wallThickness * 2, wallThickness);

			walls[5].transform.SetParent(camera.transform);
			walls[5].transform.localPosition = (bottomLeftLocal + topRightLocal) * .5f + Vector3.back * wallThickness * .5f;
			walls[5].transform.localScale = new Vector3(Width + (wallThickness - border) * 2, Height + wallThickness * 2, wallThickness);

			Environment.MinPosition = new Vector3(walls[2].transform.position.x + wallThickness * .5f, walls[0].transform.position.y + wallThickness * .5f,
				walls[5].transform.position.z + wallThickness * .5f);

			Environment.MaxPosition = new Vector3(walls[3].transform.position.x - wallThickness * .5f, walls[1].transform.position.y - wallThickness * .5f,
				walls[4].transform.position.z - wallThickness * .5f);

			foreach (var wall in walls) {
				wall.transform.SetParent(wallsParent);
			}
		}

		private void OnDrawGizmos() {
			Gizmos.color = Color.blue;
			Gizmos.DrawSphere(camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane)), .1f);
			Gizmos.DrawSphere(camera.ViewportToWorldPoint(new Vector3(0, 1, camera.nearClipPlane)), .1f);
			Gizmos.DrawSphere(camera.ViewportToWorldPoint(new Vector3(1, 0, camera.nearClipPlane)), .1f);
			Gizmos.DrawSphere(camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane)), .1f);

			Gizmos.color = Color.red;
			Gizmos.DrawSphere(Environment.MinPosition, .1f);
			Gizmos.DrawSphere(Environment.MaxPosition, .1f);
		}
	}
}