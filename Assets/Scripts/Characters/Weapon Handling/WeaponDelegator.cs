using UnityEngine;
using System;
using System.Collections;

public class WeaponDelegator : BasicBehavior {

	protected WeaponDelegate weaponDelegate;

	[SetupableField]
	public Transform weaponMount;

	public void Fire() {

		if (weaponDelegate)
			weaponDelegate.Fire();

	}

	public override void OnGameReset() { }

	public event Action<WeaponDelegator,WeaponDelegate> OnSwitchWeapon;

	public void SwitchWeapon(WeaponDelegate weaponDelegatePrefab) {

		if (weaponDelegate)
			Destroy(weaponDelegate.gameObject);

		if (weaponDelegatePrefab == null)
			return;

		weaponDelegate = Instantiate(weaponDelegatePrefab, weaponMount.position, weaponMount.rotation) as WeaponDelegate;
		weaponDelegate.transform.parent = weaponMount;

		if (OnSwitchWeapon != null)
			OnSwitchWeapon(this, weaponDelegate);

	}
	
}
