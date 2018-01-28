using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SphereCollider))]
public class Landable : MonoBehaviour {
	//Lazy initialization so that I don't have to mess with script execution order for things to find the cached collider reference.
	private SphereCollider collision;
	public SphereCollider Collision {
		get {
			if (collision == null)
				collision = GetComponent<SphereCollider> ();
			return collision;
		}
	}

	private bool infested;

	public void OnSeedHit(SeedPod pod) {
		SendMessage ("OnSeeded", pod);
	}

	public void Infest(Vector3 startingFrom, GameObject growthPrefab) {
		if (infested)
			return;

		infested = true;

		Vector3 startPoint = transform.InverseTransformPoint(startingFrom);

		List<Vector3> growthPoints = new List<Vector3> ();
		growthPoints.Add (startPoint);

		for (int i = 0; i < 200; ++i) {
			Vector3 point = Random.onUnitSphere * Collision.radius;

			bool overlap = false;

			foreach (Vector3 existingPoint in growthPoints) {
				if (Vector3.SqrMagnitude (existingPoint - point) < 0.05f) {
					overlap = true;
					break;
				}
			}

			if (overlap)
				continue;

			growthPoints.Add (point);
			GameObject g = Instantiate<GameObject> (growthPrefab, transform);
			g.transform.localPosition = point;
			Vector3 forward = Vector3.Cross (startPoint, point);
			g.transform.rotation = Quaternion.LookRotation (forward, point);
			g.transform.Rotate(g.transform.up, Random.Range(-15f, 15f));

			float delay = (Vector3.Angle (point, startPoint) / 180f);
			g.transform.DOScale (Vector3.zero, 0.2f).From ().SetDelay (delay).SetEase (Ease.OutBack);
		}
	}
}
