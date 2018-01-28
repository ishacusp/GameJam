using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CannonPlant : MonoBehaviour, IPlayerControllable {

	public Landable AttachedTo;
	public Transform CannonHead;
	public SeedPod SeedPodProjectile;

	public AudioSource _GrowSound;
	public AudioSource growSound {
		get {
			if (_GrowSound == null)
				_GrowSound = GetComponent<AudioSource> ();
			return _GrowSound;
		}
	}

	public float MinAngleDownward = 20f;
	public float MaxAngleDownward = 120f;

	private Vector3 aimDirection;

	private Tween currentAnimation;

	void Start() {
		if (AttachedTo != null) {
			Vector3 attachPoint = AttachedTo.Collision.ClosestPoint (transform.position);
			Attach (AttachedTo, attachPoint, Camera.main.transform.forward);
		}

		aimDirection = transform.forward;
		growSound.Play ();

		currentAnimation = transform.DOScale (Vector3.zero, 0.3f).From ().SetEase (Ease.OutBack);
	}

	public void Attach(Landable to, Vector3 location, Vector3 forward) {
		AttachedTo = to;

		transform.position = location;

		Vector3 outward = to.transform.InverseTransformPoint(location) - to.Collision.center;
		Vector3 parallelForward = Vector3.RotateTowards (outward, forward, 90f * Mathf.Deg2Rad, 0f);

		Debug.DrawRay (location, parallelForward * 5f, Color.green, 10f);
		Debug.DrawRay (location, outward * 5f, Color.cyan, 10f);

		transform.rotation = Quaternion.LookRotation (parallelForward, outward);
		transform.SetParent (to.transform);
	}

	void OnDrawGizmos() {
		Debug.DrawRay (CannonHead.transform.position, aimDirection * 3f, Color.red);
	}

	void Update() {
		CannonHead.transform.rotation = Quaternion.LookRotation (aimDirection, transform.up);
	}

	public void Turn(Vector2 delta) {
		Quaternion horizontalTurn = Quaternion.AngleAxis (delta.x, transform.up);
		aimDirection = horizontalTurn * aimDirection;

		Quaternion verticalTurn = Quaternion.AngleAxis (delta.y, Vector3.Cross (aimDirection, transform.up));
		aimDirection = verticalTurn * aimDirection;

		//Apply maximum limits
		aimDirection = Vector3.RotateTowards (transform.up, aimDirection, MaxAngleDownward * Mathf.Deg2Rad, 0f);
		aimDirection = Vector3.RotateTowards (-transform.up, aimDirection, (180f - MinAngleDownward) * Mathf.Deg2Rad, 0f);
	}

	public void GetCameraTransformDestination (out Vector3 position, out Vector3 up, out Vector3 forward) {
		position = CannonHead.position + (transform.up * 1f) + (aimDirection * -5f);
		forward = aimDirection;
		up = transform.up;
	}

	public void FireAction() {
		if (currentAnimation != null && currentAnimation.IsPlaying())
			return;
		
		if (SeedControl.SceneInstance.UseSeed ()) {
			ShootAnimation (() => {
				var shotParticles = CannonHead.GetComponentInChildren<ParticleSystem>();
				if (shotParticles != null)
					shotParticles.Play();
				
				SeedPod projectile = Instantiate<SeedPod> (SeedPodProjectile, CannonHead.transform.position, Quaternion.LookRotation (aimDirection, transform.up));
				projectile.Velocity = aimDirection * GameParametersControl.ProjectileSpeed;
				PlayerControl.SceneInstance.ActiveControllable = projectile;
				projectile.Creator = this;
			});
		}
	}

	private Tween ShootAnimation(TweenCallback onShoot) {
		var shootSequence = DOTween.Sequence ();

		shootSequence.Append (CannonHead.transform.DOScale (new Vector3 (0.25f, 0.25f, -0.5f), 0.2f).SetRelative().SetEase (Ease.OutQuad));
		shootSequence.Append (CannonHead.transform.DOScale (new Vector3 (-0.5f, -0.5f, 1f), 0.05f).SetRelative().SetEase (Ease.InQuad));
		shootSequence.AppendCallback (onShoot);
		shootSequence.Append (CannonHead.transform.DOScale (new Vector3 (0.25f, 0.25f, -0.5f), 0.1f).SetRelative ().SetEase (Ease.InOutQuad));

		return shootSequence;
	}

	public void SecondaryAction() {
	}
}
