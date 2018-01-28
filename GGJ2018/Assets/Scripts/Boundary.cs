using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour {
	public float WarnRadius = 30;
	public float KillRadius = 35;

	bool warned = false;

	void OnDrawGizmos() {
		Gizmos.color = new Color (1f, 0f, 0f, 0.25f);

		Gizmos.DrawWireSphere (transform.position, WarnRadius);
		Gizmos.DrawSphere (transform.position, KillRadius);
	}

	void Update() {
		var pod = (PlayerControl.SceneInstance.ActiveControllable as SeedPod);
		if (pod == null)
			return;

		float dist = Vector3.Distance (transform.position, pod.transform.position);

		if (dist > WarnRadius) {
			if (!warned) {
				NotificationControl.SceneInstance.PostNotification ("Getting close to out of bounds...");
				warned = true;
			}
		} else
			warned = false;

		if (dist > KillRadius) {
			pod.OnOutOfBounds ();
			warned = false;
		}
	}
}
