using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseControl : MonoBehaviour {
	public GameObject GameUIRoot;
	public GameObject PauseUIRoot;

	public Button ResumeButton;

	public bool Paused { get; private set; }

	public static PauseControl SceneInstance;

	void Awake() {
		if (SceneInstance == null)
			SceneInstance = this;
		else
			Destroy (this);
	}

	void Start() {
		PauseUIRoot.gameObject.SetActive (false);
		GameUIRoot.gameObject.SetActive (true);

		Time.timeScale = 1f;

		Cursor.lockState = CursorLockMode.Locked;

		Paused = false;

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

		Paused = true;
	}

	void Unpause() {
		StartCoroutine (WaitBeforeUnpause());
	}

	IEnumerator WaitBeforeUnpause() {
		yield return null;
		PauseUIRoot.gameObject.SetActive (false);
		GameUIRoot.gameObject.SetActive (true);

		Time.timeScale = 1f;

		Cursor.lockState = CursorLockMode.Locked;

		Paused = false;
	}
}
