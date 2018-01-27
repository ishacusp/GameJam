using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestablePlanet : MonoBehaviour {
	public int SeedYield;

	private bool harvested;
	public string planetName;

	void Start() {
		GoalControl.SceneInstance.RegisterPlanet (this);
		planetName = PlanetNamer.Instance.getName ();
	}

	void OnSeeded(SeedPod seedPod) {
		if (harvested)
			return;
		
		SeedControl.SceneInstance.AddSeeds (SeedYield);
		harvested = true;

		GoalControl.SceneInstance.HarvestPlanet (this);
	}
}