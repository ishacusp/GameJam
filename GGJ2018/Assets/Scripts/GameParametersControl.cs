using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParametersControl : MonoBehaviour {
	public float projectileSpeed = 2f;

	void Awake() {
		if (Instance == null) {
			Instance = this;
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
}
