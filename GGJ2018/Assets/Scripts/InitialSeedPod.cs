using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SeedPod))]
public class InitialSeedPod : MonoBehaviour {
	public Transform Target;

	void OnDrawGizmosSelected() {
		Debug.DrawLine (transform.position, Target.position, Color.yellow);
	}

	void Start() {
		SeedPod pod = GetComponent<SeedPod> ();

		if (!pod.Dummy)
			PlayerControl.SceneInstance.ActiveControllable = pod;

		pod.Velocity = (Target.position - transform.position).normalized * GameParametersControl.ProjectileSpeed;
		pod.transform.rotation = Quaternion.LookRotation (pod.Velocity, transform.up);

		Destroy (this);
	}
}
