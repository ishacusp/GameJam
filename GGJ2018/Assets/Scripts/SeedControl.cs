using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedControl : MonoBehaviour {
	public int StartingSeeds;
	public bool GodMode;

	private int seeds;
	public int Seeds {
		get {
			return seeds;
		}

		set {
			if (seeds == value)
				return;
			
			seeds = value;

			if (SeedCountUpdated != null)
				SeedCountUpdated ();
		}
	}

	public delegate void SeedEvent ();
	public event SeedEvent SeedCountUpdated;

	public static SeedControl SceneInstance { get; private set; }

	void Awake() {
		if (SceneInstance == null)
			SceneInstance = this;
		else
			Destroy (this);
	}

	void Start() {
		Seeds = StartingSeeds;
	}

	public bool CanUseSeed() {
		return (Seeds > 0 || GodMode);
	}

	public bool UseSeed() {
		if (Seeds > 0) {
			--Seeds;
			return true;
		}

		if (GodMode)
			return true;
		
		return false;
	}

	public void AddSeeds(int count) {
		Seeds += count;
	}
}
