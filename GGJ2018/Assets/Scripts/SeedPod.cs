using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SeedPod : MonoBehaviour, IPlayerControllable {
	[HideInInspector] public CannonPlant Creator;
	public Vector3 Velocity;
	public CannonPlant GrowOnLanding;

	private Transform aim;

	private Rigidbody cachedRigidbody;
	new private Rigidbody rigidbody {
		get {
			if (cachedRigidbody == null)
				cachedRigidbody = GetComponent<Rigidbody> ();
			return cachedRigidbody;
		}
	}

	void Start() {
		aim = new GameObject ("Aim").transform;
		aim.transform.SetParent (transform);
		aim.transform.localRotation = Quaternion.identity;
		aim.transform.localPosition = Vector3.zero;
	}

	void FixedUpdate() {
		Vector3 destination = transform.position + (Velocity * Time.fixedDeltaTime);
		rigidbody.MovePosition (destination);
	}

	void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag == "Boundary") {
			OnHitBoundary ();
			return;
		}

		var landable = coll.gameObject.GetComponent<Landable> ();
		if (landable != null) {
			OnLand (landable, coll.contacts[0].point);
			return;
		}
	}

	void OnHitBoundary() {
		PlayerControl.SceneInstance.ActiveControllable = Creator;
		Destroy (gameObject);
	}

	void OnLand(Landable landable, Vector3 landPoint) {
		var plant = Instantiate<CannonPlant> (GrowOnLanding);
		plant.Attach (landable, landPoint, transform.forward);
		PlayerControl.SceneInstance.ActiveControllable = plant;
		Destroy (gameObject);
	}

	#region IPlayerControllable implementation

	public void Turn (Vector2 delta)
	{
		Quaternion initial = aim.rotation;

		Quaternion horizontalTurn = Quaternion.AngleAxis (delta.x, Vector3.up);
		aim.rotation *= horizontalTurn;

		Quaternion verticalTurn = Quaternion.AngleAxis (delta.y, aim.right);
		aim.rotation *= verticalTurn;

		Quaternion destination = Quaternion.LookRotation (aim.forward, Vector3.up);
		float deltaAngle = Quaternion.Angle (aim.rotation, initial);

		aim.rotation = Quaternion.RotateTowards (aim.rotation, destination, deltaAngle);
	}

	public void GetCameraTransformDestination (out Vector3 position, out Vector3 up, out Vector3 forward)
	{
		position = transform.position + aim.forward * -4f;
		up = aim.up;
		forward = aim.forward;
	}

	public void FireAction ()
	{
		Velocity = aim.forward * Velocity.magnitude;
		transform.rotation = aim.rotation;
		aim.localRotation = Quaternion.identity;
	}

	#endregion
}
