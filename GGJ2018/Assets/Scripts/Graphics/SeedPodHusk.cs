using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SeedPodHusk : MonoBehaviour {
	[HideInInspector]
	public Vector3 Velocity;

	public float FadeTime;
	public float MaxSpinSpeed = 180f;

	private Vector3 angularVelocity;

	public void Start () {
		var fadeSequence = DOTween.Sequence ();

		foreach (Renderer r in GetComponentsInChildren<Renderer> ()) {
			fadeSequence.Join (r.material.DOFade (0f, FadeTime));
		}

		fadeSequence.AppendCallback (() => Destroy (gameObject));

		angularVelocity = Random.insideUnitSphere * MaxSpinSpeed;
	}

	void Update() {
		transform.position += (Velocity * Time.deltaTime);
		transform.rotation *= Quaternion.Euler(angularVelocity * Time.deltaTime);
	}

}
