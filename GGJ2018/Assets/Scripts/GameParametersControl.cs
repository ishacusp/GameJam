using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParametersControl : MonoBehaviour {
	public float projectileSpeed = 2f;
	public float projectiveOverspeedFriction = 1f;

	void Awake() {
		if (Instance == null) {
			Instance = this;
			transform.SetParent (null);
			DontDestroyOnLoad (gameObject);
		}
		else
			Destroy (gameObject);
	}

	private static GameParametersControl Instance;

	public static float ProjectileSpeed {
		get {
			return Instance.projectileSpeed;
		}
	}

	public static float ProjectileOverspeedFriction {
		get {
			return Instance.projectiveOverspeedFriction;
		}
	}
}
