using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxCrossfade : MonoBehaviour {
	public Material material;
	public float transitionDuration = 2; // seconds
	public float pauseDuration = 0; // seconds
	private DateTime startTime;
	private string state = "transitioning";
	public Material[] materials;
	public int currentSkyboxIndex = 0;
	private string[] sides = new string[]{"Front", "Back", "Left", "Right", "Up", "Down"};

//	void Update
		
	void Start() {
		material = RenderSettings.skybox;
		startTime = DateTime.Now;

		// if no skybox, don't do anything
		if (material == null)
			state = "missing";
		// don't do anything if it's the wrong skybox type
		else if (material.shader != Shader.Find( "Skybox/Blended" ))
			state = "incompatible";
	}

	void Update() {
		if (state == "transitioning") {
			float delta = (float)DateTime.Now.Subtract (startTime).TotalSeconds;
			if (delta < transitionDuration) {
				// mid transition
				material.SetFloat ("_Blend", delta / transitionDuration);
			} else {
				// finished transition
				state = "pausing";
			}
		} else if (state == "pausing") {
			double delta = DateTime.Now.Subtract (startTime).TotalSeconds;
			if (delta > pauseDuration) {
				startTime = DateTime.Now;
				state = "transitioning";

				// swap the skyboxes
				material.SetFloat ("_Blend", 0);
				currentSkyboxIndex = (currentSkyboxIndex + 1) % materials.Length;
				Material newMaterial = materials[currentSkyboxIndex];
				foreach (string side in sides){
					material.SetTexture("_" + side + "Tex", material.GetTexture("_" + side + "Tex2"));
					material.SetTexture("_" + side + "Tex2", newMaterial.GetTexture("_" + side + "Tex"));
				}
			}
		}
	}
}