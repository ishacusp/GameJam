using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerControllable {
	void Turn (Vector2 delta);
	void GetCameraTransformDestination (out Vector3 position, out Vector3 up, out Vector3 forward);
	void FireAction();
}

public class PlayerControl : MonoBehaviour {

	public float InputMultiplier = 1f;
	public float CamFollowSpeedMultiplier = 5f;

	private Transform cameraTarget;

	public IPlayerControllable ActiveControllable;

	public static PlayerControl SceneInstance { get; private set; }

	void Awake() {
		if (SceneInstance == null)
			SceneInstance = this;
		else 
			Destroy (this);
	}

	void Start() {
		Cursor.lockState = CursorLockMode.Locked;

		cameraTarget = new GameObject ("Camera Target").transform;
	}

	void Update() {
		if (PauseControl.SceneInstance != null && PauseControl.SceneInstance.Paused)
			return;

		Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

		if (ActiveControllable != null) {
			ActiveControllable.Turn (mouseDelta * InputMultiplier);

			if (Input.GetMouseButtonDown (0)) {
				ActiveControllable.FireAction ();
			}
		}
	}

	void LateUpdate() {
		if (ActiveControllable != null) {
			Vector3 pos, up, forward;
			ActiveControllable.GetCameraTransformDestination (out pos, out up, out forward);

			cameraTarget.transform.SetPositionAndRotation (
				pos,
				Quaternion.LookRotation (forward, up)
			);
		}

		float camAngleOffset = Quaternion.Angle (Camera.main.transform.rotation, cameraTarget.transform.rotation);
		float camPositionOffset = Vector3.Distance (Camera.main.transform.position, cameraTarget.transform.position);

		bool snapCameraRotation = camAngleOffset < 0.1f;
		bool snapCameraPosition = camPositionOffset < 0.1f;

		if (snapCameraPosition && snapCameraRotation) {
			Camera.main.transform.position = cameraTarget.transform.position;
			Camera.main.transform.rotation = cameraTarget.transform.rotation;
		} else {
			Camera.main.transform.rotation = 
				Quaternion.RotateTowards (Camera.main.transform.rotation, cameraTarget.transform.rotation, camAngleOffset * CamFollowSpeedMultiplier * Time.deltaTime);
			Camera.main.transform.position = 
				Vector3.MoveTowards (Camera.main.transform.position, cameraTarget.transform.position, camPositionOffset * CamFollowSpeedMultiplier * Time.deltaTime);
		}
	}
}
