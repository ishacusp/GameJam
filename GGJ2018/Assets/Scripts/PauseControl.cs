using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseControl : MonoBehaviour {
	public GameObject GameUIRoot;
	public GameObject PauseUIRoot;

	public Button ResumeButton;

	void Start() {
		Unpause ();

		ResumeButton.onClick.AddListener (Unpause);
	}

	void Update () {
		if (Cursor.lockState != CursorLockMode.Locked)
			Pause ();
	}

	void Pause() {
		PauseUIRoot.gameObject.SetActive (true);
		GameUIRoot.gameObject.SetActive (false);

		Time.timeScale = 0f;

		Cursor.lockState = CursorLockMode.None;
	}

	void Unpause() {
		PauseUIRoot.gameObject.SetActive (false);
		GameUIRoot.gameObject.SetActive (true);

		Time.timeScale = 1f;

		Cursor.lockState = CursorLockMode.Locked;
	}
}
