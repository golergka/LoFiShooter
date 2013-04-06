using UnityEngine;
using System;
using System.Collections;

public abstract class WeaponDelegate : BasicBehavior {

	public float firePeriod = 1f;
	public float cameraRecoil = 1f;

	public SoundEvent shootSound;

	float lastFireTime = -1f;

	protected abstract void Shoot();

	public event Action<WeaponDelegate, float> OnRecoil;

	public Renderer flare;
	public float flareTime = 0.1f;

	public void Fire() {

		if ( lastFireTime < 0 || (Time.time - lastFireTime) > firePeriod ) {

			lastFireTime = Time.time;

			shootSound.Play(this);

			if (OnRecoil != null)
				OnRecoil(this, cameraRecoil);

			Shoot();

		}

	}

	const string FLARE_COLOR_PROPERTY = "_TintColor";

	void Update() {

		if (flare) {

			Color flareColor = flare.material.GetColor(FLARE_COLOR_PROPERTY);
			flareColor.a = 1 - Mathf.Min( (Time.time - lastFireTime)/flareTime, 1f);
			flare.material.SetColor(FLARE_COLOR_PROPERTY, flareColor);

		}

	}

}