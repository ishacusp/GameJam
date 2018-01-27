using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	public void OnSeedHit(SeedPod pod) {
		SendMessage ("OnSeeded", pod);
	}
}
