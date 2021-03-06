﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlanetSeedYieldIndicator : MonoBehaviour {
	public RectTransform DisplaySeed;
	public RectTransform SeedIndicatorRegion;
	public CanvasGroup GraphicRoot;
	public float VanishLimit;

	private bool visible;
	private Tween visibilityTween;
	private HarvestablePlanet planet;
	private bool generatedSeeds;

	void Start() {
		planet = GetComponentInParent<HarvestablePlanet> ();

		visible = false;
		GraphicRoot.alpha = 0f;
	}

	void Update () {
		if (planet.Harvested) {
			Destroy (gameObject);
			return;
		}

		if (!generatedSeeds) {
			for (int i = 0; i < planet.SeedYield; ++i) {
				Instantiate (DisplaySeed, SeedIndicatorRegion);
			}
			generatedSeeds = true;
		}

		transform.position = planet.transform.position + Camera.main.transform.up * (planet.Bounds.extents.x + 0.25f);

		Vector3 camOffset = transform.position - Camera.main.transform.position;

		transform.rotation = Quaternion.LookRotation (camOffset, Camera.main.transform.up);

		if (Application.isPlaying) {
			float dist = camOffset.magnitude;

			var m = (PlayerControl.SceneInstance.ActiveControllable as MonoBehaviour);
			if (m != null && m.transform != null) {
				dist = Mathf.Min (
					            Vector3.Distance (transform.position, (PlayerControl.SceneInstance.ActiveControllable as MonoBehaviour).transform.position),
					            dist
				            );
			}

			if (visible && dist < VanishLimit) {
				if (visibilityTween != null)
					visibilityTween.Kill ();

				visibilityTween = GraphicRoot.DOFade (0f, 0.2f);
				visible = false;
			} else if (!visible && dist > VanishLimit) {
				if (visibilityTween != null)
					visibilityTween.Kill ();

				visibilityTween = GraphicRoot.DOFade (1f, 0.2f);
				visible = true;
			}
		} else {
			if (GraphicRoot != null)
				GraphicRoot.alpha = 1f;
		}
	}
}
