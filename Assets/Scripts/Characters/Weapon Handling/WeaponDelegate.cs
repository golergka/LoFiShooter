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

	public void Fire() {

		if ( lastFireTime < 0 || (Time.time - lastFireTime) > firePeriod ) {

			lastFireTime = Time.time;

			shootSound.Play(this);
			if (OnRecoil != null)
				OnRecoil(this, cameraRecoil);

			Shoot();

		}

	}

}