using UnityEngine;
using System.Collections;

public class BulletGenerator : WeaponDelegate {

	public Transform muzzle;

	public Transform bullet;

	public float firePeriod = 1f;

	void Awake() {

		if (!muzzle) {

			Debug.LogWarning("Please, set up muzzle!");
			enabled = false;

		}

		if (!bullet) {

			Debug.LogWarning("Please, set up bullet!");
			enabled = false;

		}

	}

	public override void Fire() {

		// Not using it

	}

	float lastFireTime = -1;

	public override void FireContinuous() {

		if ( lastFireTime < 0 || (Time.time - lastFireTime) > firePeriod ) {

			Instantiate(bullet, muzzle.position, muzzle.rotation);

		}

	}

}
