using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxCrossfade : MonoBehaviour {
	private Material material;
	public float transitionDuration = 8; // seconds
	public float pauseDuration = 2; // seconds
	private DateTime startTime;
	private string state = "transitioning";

//	void Update
		
	void Start() {
		material = RenderSettings.skybox;
		startTime = DateTime.Now;
	}

	void Update() {
		
		if (state == "transitioning") {
			float delta = (float)DateTime.Now.Subtract (startTime).TotalSeconds;
			if (delta <= transitionDuration) {
				// mid transition

				// this is not pretty or flexible ik :(
//				material.shader._FrontTex = material.shader._FrontTex2;
//				material.shader._BackTex = material.shader._FrontTex2;
//				material.shader._LeftTex = material.shader._FrontTex2;
//				material.shader._RightTex = material.shader._FrontTex2;
//				material.shader._UpTex = material.shader._UpTex2;
//				material.shader._DownTex = material.shader._DownTex2;
				material.SetFloat ("_Blend", delta / transitionDuration);
				Debug.Log (delta / transitionDuration);
			} else {
				// finished transition
				state = "pausing";
			}
		} else if (state == "pausing") {
			double delta = DateTime.Now.Subtract (startTime).TotalSeconds;
			if (delta >= pauseDuration) {
				startTime = DateTime.Now;
				state = "transitioning";
			}
		}
	}
}