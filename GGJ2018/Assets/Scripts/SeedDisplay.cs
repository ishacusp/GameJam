using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO rework into something more attractive?
public class SeedDisplay : MonoBehaviour {
	public Text Display;

	void OnEnable() {
		SeedControl.SceneInstance.SeedCountUpdated += UpdateDisplay;
		UpdateDisplay ();
	}

	void OnDisable() {
		SeedControl.SceneInstance.SeedCountUpdated -= UpdateDisplay;
	}

	void UpdateDisplay() {
		Display.text = string.Format ("Seeds remaining: {0}", SeedControl.SceneInstance.Seeds);
	}
}
