using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SeedPod : MonoBehaviour, IPlayerControllable, IBlackHoleCapturable {
	[HideInInspector] public CannonPlant Creator;
	public Vector3 Velocity;
	public CannonPlant GrowOnLanding;

	private Transform aim;

	private HashSet<BlackHole> pulledBy = new HashSet<BlackHole>();

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
		//Hit the brakes if it's out of all gravity wells
		if (pulledBy.Count == 0) {
			Vector3 clampedVelocity = Vector3.ClampMagnitude (Velocity, GameParametersControl.ProjectileSpeed);
			Velocity = Vector3.MoveTowards (Velocity, clampedVelocity, GameParametersControl.ProjectileOverspeedFriction * Time.deltaTime);
		}

		Vector3 destination = transform.position + (Velocity * Time.fixedDeltaTime);
		rigidbody.MovePosition (destination);
	}

	void OnCollisionEnter(Collision coll) {
		switch (coll.gameObject.tag) {
		case "Sun":
			OnHitSun ();
			break;
		case "Black Hole":
			OnHitBlackHole ();
			break;
		case "Boundary":
			ReturnControl ();
			break;
		}

		var landable = coll.gameObject.GetComponent<Landable> ();
		if (landable != null) {
			OnLand (landable, coll.contacts[0].point);
			return;
		}
	}

	void OnHitSun() {
		NotificationControl.SceneInstance.PostNotification ("Watch it, Icarus.", new Color (1f, 0.65f, 0.13f));
		ReturnControl ();
	}

	void OnHitBlackHole() {
		NotificationControl.SceneInstance.PostNotification ("Black holes really suck, huh?", Color.magenta);
		ReturnControl ();
	}

	void ReturnControl() {
		PlayerControl.SceneInstance.ActiveControllable = Creator;
		Destroy (gameObject);
	}

	void OnLand(Landable landable, Vector3 landPoint) {
		landable.OnSeedHit (this);

		var plant = Instantiate<CannonPlant> (GrowOnLanding);
		plant.Attach (landable, landPoint, transform.forward);
		PlayerControl.SceneInstance.ActiveControllable = plant;
		Destroy (gameObject);
	}

	#region IPlayerControllable implementation

	public void Turn (Vector2 delta)
	{
		Quaternion rotation = Quaternion.Euler (delta.y, delta.x, 0f);

		aim.rotation *= rotation;
	}

	public void GetCameraTransformDestination (out Vector3 position, out Vector3 up, out Vector3 forward)
	{
		position = transform.position + aim.forward * -4f;
		up = aim.up;
		forward = aim.forward;
	}

	public void FireAction ()
	{
		if (SeedControl.SceneInstance.UseSeed ()) {
			Velocity = aim.forward * Velocity.magnitude;
			transform.rotation = aim.rotation;
			aim.localRotation = Quaternion.identity;
		}
	}

	public void SecondaryAction() {
		if (Creator != null) {
			ReturnControl ();
		}
	}

	#endregion

	#region IBlackHoleCapturable implementation

	public void PullTowards (BlackHole blackHole, float pullMagnitude)
	{		
		if (this == null)	//Dumb check because Unity calls this even if it destroys the object
			return;
		
		Vector3 pull = blackHole.transform.position - transform.position;
		Velocity += pull.normalized * pullMagnitude;
	}

	public void OnEnterPullRegion (BlackHole blackHole) {
		pulledBy.Add (blackHole);
	}

	public void OnExitPullRegion (BlackHole blackHole) {
		pulledBy.Remove (blackHole);
	}

	#endregion
}
