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

	public delegate void PauseEvent();
	public event PauseEvent OnPaused, OnUnpaused;

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
		Cursor.visible = false;

		Paused = false;

		ResumeButton.onClick.AddListener (Unpause);
	}

	void Update () {
		if (Input.GetKey (KeyCode.Escape))
			Pause ();
		else if (Cursor.lockState != CursorLockMode.Locked)
			Pause ();
	}

	void Pause() {
		PauseUIRoot.gameObject.SetActive (true);
		GameUIRoot.gameObject.SetActive (false);

		Time.timeScale = 0f;

		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;

		Paused = true;

		if (OnPaused != null)
			OnPaused ();
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
		Cursor.visible = false;

		Paused = false;

		if (OnUnpaused != null)
			OnUnpaused ();
	}
}
