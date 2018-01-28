using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour {
	public Text DisplayText;
	public float DisplayTime = 2f;
	public float FadeTime = 0.25f;

	Coroutine fadeRoutine;

	void Start() {
		StartCoroutine (FadeAfterDelay ());
	}

	void OnEnable() {
		PauseControl.SceneInstance.OnUnpaused += OnUnpaused;
	}

	void OnDisable() {
		PauseControl.SceneInstance.OnUnpaused -= OnUnpaused;
	}

	void OnUnpaused() {
		StartCoroutine (FadeAfterDelay ());
	}

	IEnumerator FadeAfterDelay () {
		yield return new WaitForSeconds (DisplayTime);

		for (float t = 0f; t < FadeTime; t += Time.deltaTime) {
			yield return new WaitForEndOfFrame ();
			DisplayText.color = new Color (DisplayText.color.r, DisplayText.color.g, DisplayText.color.b, 1f - (t / FadeTime));
		}

		Destroy (gameObject);
	}
}
