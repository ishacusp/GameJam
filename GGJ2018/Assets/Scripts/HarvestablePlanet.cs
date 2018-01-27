using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestablePlanet : MonoBehaviour {
	public int SeedYield;

	new private Renderer renderer;
	public bool Harvested { get; private set; }

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
		if (Harvested)
			return;
		
		SeedControl.SceneInstance.AddSeeds (SeedYield);
		Harvested = true;

		GoalControl.SceneInstance.HarvestPlanet (this);
	}
}