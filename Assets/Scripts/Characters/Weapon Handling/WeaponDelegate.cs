using UnityEngine;
using System.Collections;

public abstract class WeaponDelegate : MonoBehaviour {

	public float firePeriod = 1f;
	float lastFireTime = -1f;

	protected abstract void Shoot();

	public void Fire() {

		if ( lastFireTime < 0 || (Time.time - lastFireTime) > firePeriod ) {

			lastFireTime = Time.time;

			Shoot();

		}

	}

}