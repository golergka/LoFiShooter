using UnityEngine;
using System.Collections;

public class WeaponDelegator : MonoBehaviour {

	protected WeaponDelegate weaponDelegate;

	public Transform weaponMount;

	void Awake() {

		if (!weaponMount) {

			Debug.LogWarning("Please, configure weapon mount!");
			enabled = false;

		}

	}

	public void Fire() {

		if (weaponDelegate)
			weaponDelegate.Fire();

	}

	public void SwitchWeapon(WeaponDelegate weaponDelegatePrefab) {

		if (weaponDelegate)
			Destroy(weaponDelegate.gameObject);

		if (weaponDelegatePrefab == null)
			return;

		weaponDelegate = Instantiate(weaponDelegatePrefab, weaponMount.position, weaponMount.rotation) as WeaponDelegate;
		weaponDelegate.transform.parent = weaponMount;

	}
	
}
