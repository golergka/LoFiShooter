using UnityEngine;
using System.Collections;

public class BulletGenerator : WeaponDelegate {

	public Transform muzzle;

	public Transform bullet;

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

	protected override void Shoot() {

		Instantiate(bullet, muzzle.position, muzzle.rotation);

	}

}
