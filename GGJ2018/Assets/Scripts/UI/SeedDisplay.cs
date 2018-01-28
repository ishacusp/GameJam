using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SeedDisplay : MonoBehaviour {
	public RectTransform Graphic;
	public Text Display;

	private int prevSeedCount;

	void OnEnable() {
		prevSeedCount = SeedControl.SceneInstance.Seeds;

		SeedControl.SceneInstance.SeedCountUpdated += UpdateDisplay;

		UpdateDisplay ();
	}

	void OnDisable() {
		SeedControl.SceneInstance.SeedCountUpdated -= UpdateDisplay;
	}

	void UpdateDisplay() {
		int seeds = SeedControl.SceneInstance.Seeds;

		Display.text = seeds.ToString();

		Graphic.transform.DOKill (true);

		if (seeds > prevSeedCount) {
			Graphic.transform.DOPunchScale (Graphic.transform.localScale * 0.4f, 0.5f, 15);
		} else if (seeds < prevSeedCount) {
			if (seeds > 0)
				Graphic.transform.DOPunchRotation (Vector3.forward * 20f, 0.3f, 10);
			else
				Graphic.transform.DOPunchRotation (Vector3.forward * 40f, 0.5f, 15);
		}

		prevSeedCount = seeds;
	}
}
