using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSeed : MonoBehaviour {

	public Vector3 FlySpeed;
	public Transform Roll;
	public Vector3 RollSpeed;

	void Update () {
		Vector3 fly = transform.TransformVector (FlySpeed);

		transform.position += fly * Time.deltaTime;
		Roll.transform.Rotate (RollSpeed * Time.deltaTime);
	}
}
