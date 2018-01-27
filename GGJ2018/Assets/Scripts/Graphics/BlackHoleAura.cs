using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleAura : MonoBehaviour {
	public float SpinSpeed;
	public float DeltaSpinSpeed;

	Vector3 currentSpin;

	void Start() {
		currentSpin = Random.onUnitSphere * SpinSpeed;
	}

	void Update () {
		Quaternion r = Quaternion.Euler (currentSpin * Time.deltaTime);

		transform.rotation *= r;

		Vector3 randomSpin = Random.onUnitSphere * SpinSpeed;
		currentSpin = Vector3.RotateTowards (currentSpin, randomSpin, DeltaSpinSpeed * Mathf.Deg2Rad * Time.deltaTime, 0f);
	}
}
