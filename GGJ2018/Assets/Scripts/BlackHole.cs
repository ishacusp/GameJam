using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBlackHoleCapturable {
	void OnEnterPullRegion (BlackHole blackHole);
	void OnExitPullRegion (BlackHole blackHole);
	void PullTowards(BlackHole blackHole, float pullMagnitude);
}

public class BlackHole : MonoBehaviour {
	public float Radius;
	public float PullSpeed;
	public ParticleSystem AbsorbingParticles;
	public WindZone ParticleWindZone;
	public SphereCollider EffectZone;

	private HashSet<IBlackHoleCapturable> capturing = new HashSet<IBlackHoleCapturable> ();

	void OnDrawGizmosSelected() {
		Gizmos.color = new Color (1f, 0f, 1f, 0.25f);
		Gizmos.DrawSphere (transform.position, Radius);
	}

	void Start() {
		ParticleWindZone.radius = Radius + 0.1f;
		EffectZone.radius = Radius;
		var shape = AbsorbingParticles.shape;
		shape.radius = Radius;
	}

	void OnTriggerEnter(Collider collider) {
		IBlackHoleCapturable capture = collider.GetComponentInParent<IBlackHoleCapturable> ();

		if (capture != null) {
			capturing.Add (capture);

			capture.OnEnterPullRegion (this);
		}
	}

	void OnTriggerExit(Collider collider) {
		IBlackHoleCapturable capture = collider.GetComponentInParent<IBlackHoleCapturable> ();

		if (capture != null) {
			capture.OnExitPullRegion (this);

			capturing.Remove (capture);
		}
	}

	void FixedUpdate() {
		capturing.RemoveWhere (c => c == null);

		foreach (IBlackHoleCapturable capturable in capturing) 
			capturable.PullTowards (this, PullSpeed * Time.fixedDeltaTime);
	}
}
