using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestablePlanet : MonoBehaviour {
	public int SeedYield;

	new private Renderer renderer;
	public bool Harvested { get; private set; }
	public string planetName;

	public Bounds Bounds {
		get {
			if (renderer == null)
				renderer = GetComponentInChildren<Renderer> ();
			return renderer.bounds;
		}
	}

	void Start() {
		if (GoalControl.SceneInstance != null)
			GoalControl.SceneInstance.RegisterPlanet (this);
		
		if (PlanetNamer.Instance != null)
			planetName = PlanetNamer.Instance.getName ();
	}

	void OnSeeded(SeedPod seedPod) {
		if (Harvested)
			return;

		if (SeedControl.SceneInstance != null)
			SeedControl.SceneInstance.AddSeeds (SeedYield);
	
		Harvested = true;

		if (GoalControl.SceneInstance != null)
			GoalControl.SceneInstance.HarvestPlanet (this);
	}
}