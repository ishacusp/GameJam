using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestablePlanet : MonoBehaviour {
	public int SeedYield;

	new private Renderer renderer;
	private bool harvested;

	public Bounds Bounds {
		get {
			if (renderer == null)
				renderer = GetComponentInChildren<Renderer> ();
			return renderer.bounds;
		}
	}

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