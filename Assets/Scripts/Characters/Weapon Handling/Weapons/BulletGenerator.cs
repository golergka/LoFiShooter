using UnityEngine;
using System.Collections;

public class BulletGenerator : WeaponDelegate {

	[SetupableField]
	public Transform muzzle;

	[SetupableField]
	public Transform bullet;

	protected override void Shoot() {

		Instantiate(bullet, muzzle.position, muzzle.rotation);

	}

}
