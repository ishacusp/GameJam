using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestablePlanet : MonoBehaviour {
	public int SeedYield;

	void OnSeeded(SeedPod seedPod) {
		SeedControl.SceneInstance.AddSeeds (SeedYield);
		SeedYield = 0;
		//TODO visual display to reflect harvesting
	}
}