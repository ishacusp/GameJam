using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestablePlanet : MonoBehaviour {
	public int SeedYield;

	private bool harvested;

	void Start() {
		GoalControl.SceneInstance.RegisterPlanet (this);
	}

	void OnSeeded(SeedPod seedPod) {
		if (harvested)
			return;
		
		SeedControl.SceneInstance.AddSeeds (SeedYield);
		harvested = true;

		GoalControl.SceneInstance.HarvestPlanet (this);
	}
}