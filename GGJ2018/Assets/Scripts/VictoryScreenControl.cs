using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class VictoryScreenControl : MonoBehaviour {
	public float DelayBeforeInput = 3f;
	public Text InputText;

	bool inputEnabled = false;

	IEnumerator Start() {
		InputText.DOFade (0f, 0f);

		yield return new WaitForSeconds (DelayBeforeInput);

		inputEnabled = true;

		InputText.DOFade (1f, 0.2f);
	}

	void Update() {
		if (!inputEnabled)
			return;

		if (Input.GetMouseButtonDown (0))
			SceneManager.LoadScene (0);
	}
}
