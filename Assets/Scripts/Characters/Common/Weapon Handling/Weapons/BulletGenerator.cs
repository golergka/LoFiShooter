using UnityEngine;
using System.Collections;

public class BulletGenerator : WeaponDelegate {

	[SetupableField]
	public Transform muzzle;

	[SetupableField]
	public Transform bullet;

	public override void OnGameReset() { }

	protected override void Shoot() {

		Instantiate(bullet, muzzle.position, muzzle.rotation);

	}

}
