using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OffscreenPlanetIndicator : MonoBehaviour {
	public CanvasGroup GraphicRoot;
	public Text PlanetNameDisplay;
	public Text SeedYieldDisplay;

	public HarvestablePlanet Planet;

	private RectTransform rt;

	Vector3 GetPlanetCameraLocation() {
		return Camera.main.WorldToViewportPoint (Planet.transform.position);
	}

	void Start() {
		rt = GraphicRoot.GetComponent<RectTransform> ();
		PlanetNameDisplay.text = Planet.name;
		SeedYieldDisplay.text = string.Format ("{0} seed yield", Planet.SeedYield);
	}

	void DisplayAlongScreenEdge(Vector2 screenEdge) {
		rt.anchorMin = rt.anchorMax = rt.pivot = screenEdge;
		rt.anchoredPosition = Vector2.zero;
	}

	void Update () {
		Vector3 planetLoc = GetPlanetCameraLocation ();

		if (planetLoc.x >= 0f && planetLoc.x <= 1f && planetLoc.y >= 0f && planetLoc.y <= 1f) 
			GraphicRoot.alpha = 0f;
		else {
			GraphicRoot.alpha = 1f;
			DisplayAlongScreenEdge (calculateScreenEdge ());
		}
	}

	Vector2 calculateScreenEdge() {
		Vector3 v = Camera.main.transform.InverseTransformVector (Planet.transform.position - Camera.main.transform.position);

		Vector3 vx = new Vector3 (v.x, 0f, v.z);
		Vector3 vy = new Vector3 (0f, v.y, v.z);

		float xx = Vector3.SignedAngle (Vector3.forward, vx, Vector3.up);
		float yy = Vector3.SignedAngle (Vector3.forward, vy, Vector3.left);

		float xxx = xx < 0f ? -180f - xx : 180f - xx;
		float yyy = yy < 0f ? -180f - yy : 180f - yy;

		float x, y;

		if (Mathf.Abs (xx) > Mathf.Abs (yy)) {
			y = Mathf.Sign (yy);
			x = xxx / Mathf.Abs(yyy);
		} else {
			x = Mathf.Sign (xx);
			y = yyy / Mathf.Abs(xxx);
		}

		return new Vector2 (
			Mathf.InverseLerp(-1f, 1f, x), 
			Mathf.InverseLerp(-1f, 1f, y)
		);
	}
}
