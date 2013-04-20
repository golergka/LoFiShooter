using UnityEngine;
using System;
using System.Collections;

public abstract class WeaponDelegate : BasicBehavior {

	// === Gun properties ===

	public float firePeriod = 1f;
	public float recoil = 1f;

	public SoundEvent shootSound;

	public float pushForce;

	// === GUI ===

	// Name to be used in GUI. Can be changed to localizable type later.
	// Can be ommited for weapons that AI use.
	public string guiName;

	// Can be ommited for weapons that AI use.
	public Texture guiIcon;

	// === Effects ===

	public ParticleSystem shellEmitter;

	// muzzle flare
	public Renderer flare;
	public float flareTime = 0.1f;

	// muzze light
	public Light muzzleLight;
	public float lightIntensity = 0.5f;
	public float lightTime = 0.1f;

	protected abstract void Shoot();

	public event Action<WeaponDelegate, float> OnRecoil;

	float lastFireTime = -1f;

	public void Fire() {

		if ( lastFireTime < 0 || (Time.time - lastFireTime) > firePeriod ) {

			lastFireTime = Time.time;

			shootSound.Play(this);

			if (OnRecoil != null)
				OnRecoil(this, recoil);

			if (flare) {

				Vector3 flareRotation = flare.transform.eulerAngles;

				flareRotation.z = UnityEngine.Random.Range(0f,360f);

				flare.transform.eulerAngles = flareRotation;

			}

			if (shellEmitter) {

				shellEmitter.Emit(1);

			}

			if (muzzleLight) {

				muzzleLight.intensity = lightIntensity;

			}

			Shoot();

		}

	}

	static readonly string[] FLARE_COLOR_PROPERTY = { "_TintColor", "_Color" };
	const float  FLARE_MAX_ALPHA = 0.5f;

	void Update() {

		if (flare) {

			string colorProperty = null;

			foreach(string p in FLARE_COLOR_PROPERTY) {

				if (flare.material.HasProperty(p)) {
					colorProperty = p;
					break;
				}

			}

			if (colorProperty != null) {

				Color flareColor = flare.material.GetColor(colorProperty);
				flareColor.a = FLARE_MAX_ALPHA - Mathf.Min( (Time.time - lastFireTime)/flareTime, FLARE_MAX_ALPHA);
				flare.material.SetColor(colorProperty, flareColor);

			} else {

				Debug.LogWarning("Muzzle flash shader doesn't have color property I can use!");

			}

		}

		if (muzzleLight) {

			muzzleLight.intensity = lightIntensity * (1 - Mathf.Max( (Time.time - lastFireTime)/lightTime, 0 ) );

		}

	}

	public ParticleSystem hitEffect;

	protected void Hit(GameObject target, Vector3 point, Vector3 direction, Vector3 normal, int damage) {

		// Instantiating hit effect

		if (hitEffect) {
			Quaternion hitRotation = Quaternion.identity;
			hitRotation.SetLookRotation(normal);
			Instantiate(hitEffect, point, hitRotation);
		}

		// Inflicting damage

		IDamageReceiver targetDamageReceiver = (IDamageReceiver) target.GetComponent(typeof(IDamageReceiver));

		if (targetDamageReceiver != null) {
			targetDamageReceiver.InflictDamage(damage);
		}

		// Pushing target away

		Rigidbody targetRigidbody = target.GetComponent<Rigidbody>();

		if (targetRigidbody && pushForce > 0f) {

			Vector3 pushForceVector = direction;
			pushForceVector.Normalize();
			pushForceVector *= pushForce;

			targetRigidbody.AddForce(pushForceVector);

		}

	}

}