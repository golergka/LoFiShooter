using UnityEngine;
using System;
using System.Collections;

public abstract class WeaponDelegate : BasicBehavior {

	public float firePeriod = 1f;
	public float recoil = 1f;

	public SoundEvent shootSound;

	float lastFireTime = -1f;

	protected abstract void Shoot();

	public event Action<WeaponDelegate, float> OnRecoil;

	public Renderer flare;
	public float flareTime = 0.1f;

	// Name to be used in GUI. Can be changed to localizable type later.
	// Can be ommited for weapons that AI use.
	public string guiName;

	// Can be ommited for weapons that AI use.
	public Texture guiIcon;

	ParticleSystem shellEmitter;

	override protected void Awake() {

		base.Awake();

		shellEmitter = GetComponentInChildren<ParticleSystem>();

	}

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

			Shoot();

		}

	}

	const string FLARE_COLOR_PROPERTY = "_TintColor";
	const float  FLARE_MAX_ALPHA = 0.5f;

	void Update() {

		if (flare) {

			Color flareColor = flare.material.GetColor(FLARE_COLOR_PROPERTY);
			flareColor.a = FLARE_MAX_ALPHA - Mathf.Min( (Time.time - lastFireTime)/flareTime, FLARE_MAX_ALPHA);
			flare.material.SetColor(FLARE_COLOR_PROPERTY, flareColor);

		}

	}

}