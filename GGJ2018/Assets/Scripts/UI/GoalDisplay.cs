using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalDisplay : MonoBehaviour {
	public Text Display;

	void OnEnable() {
		GoalControl.SceneInstance.PlanetCountUpdated += UpdateDisplay;
		UpdateDisplay ();
	}

	void OnDisable() {
		GoalControl.SceneInstance.PlanetCountUpdated -= UpdateDisplay;
	}

	void UpdateDisplay() {
		Display.text = string.Format ("Planets Remaining: {0}", GoalControl.SceneInstance.RemainingPlanets);
	}
}
