using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetNamer : MonoBehaviour {
	public List<string> possibleNames;

	void Awake() {
		if (Instance == null) {
			Instance = this;
			transform.SetParent (null);
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
			return;
		}
	}

//	void Start() {
//	}

	public static PlanetNamer Instance;

	public TextAsset names;

	private void getNameList(){
		possibleNames = names.text.Split('\n').ToList();
	}

	public string getName() {
		if (possibleNames.Count == 0)
			getNameList ();

		int i = Random.Range (0, possibleNames.Count);
		string name = possibleNames [i];
		possibleNames.RemoveAt (i);
		return name;
	}

}
