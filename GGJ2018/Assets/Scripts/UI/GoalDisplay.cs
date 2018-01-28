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
		int total = GoalControl.SceneInstance.TotalPlanets;
		int harvested = total - GoalControl.SceneInstance.RemainingPlanets;
		Display.text = string.Format ("{0}/{1}", harvested, total);
	}
}
