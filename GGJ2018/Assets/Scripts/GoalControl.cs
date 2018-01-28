using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalControl : MonoBehaviour {

	private HashSet<HarvestablePlanet> remainingPlanets = new HashSet<HarvestablePlanet>();

	public int RemainingPlanets {
		get {
			return remainingPlanets.Count;
		}
	}

	public int TotalPlanets { get; private set; }

	public static GoalControl SceneInstance;

	public delegate void GoalProgressEvent();
	public event GoalProgressEvent PlanetCountUpdated;
	public event GoalProgressEvent AllPlanetsHarvested;

	void Awake() {
		if (SceneInstance == null)
			SceneInstance = this;
		else
			Destroy (this);
	}

	public void RegisterPlanet(HarvestablePlanet planet) {
		remainingPlanets.Add (planet);

		TotalPlanets = Mathf.Max (TotalPlanets, RemainingPlanets);

		if (PlanetCountUpdated != null)
			PlanetCountUpdated ();
	}

	public void HarvestPlanet(HarvestablePlanet planet) {

		NotificationControl.SceneInstance.PostNotification (string.Format ("Harvested {0}!", planet.planetName));

		remainingPlanets.Remove (planet);

		if (PlanetCountUpdated != null)
			PlanetCountUpdated ();
		
		if (remainingPlanets.Count == 0) {
			if (AllPlanetsHarvested != null)
				AllPlanetsHarvested ();
		}
	}
}
