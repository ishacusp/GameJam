using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenControl : MonoBehaviour {
	public GameObject Instructions;

	void Start() {
		Instructions.gameObject.SetActive (false);
	}

	void Update () {
		if (Input.GetMouseButtonDown (0)) {	
			if (Instructions.gameObject.activeSelf)
				StartGame ();
			else
				Instructions.gameObject.SetActive (true);
		}

		if (Input.GetMouseButton (1)) {
			Instructions.gameObject.SetActive (false);
		}
	}

	void StartGame() {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}
}
