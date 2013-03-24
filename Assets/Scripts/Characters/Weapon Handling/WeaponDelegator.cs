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

		weaponDelegate.Fire();

	}

	public void FireContinuous() {

		weaponDelegate.FireContinuous();
		
	}

	public void SwitchWeapon(WeaponDelegate weaponDelegatePrefab) {

		Destroy(weaponDelegate);

		if (weaponDelegatePrefab == null)
			return;

		weaponDelegate = Instantiate(weaponDelegatePrefab, weaponMount.position, weaponMount.rotation) as WeaponDelegate;
		weaponDelegate.transform.parent = weaponMount;

	}
	
}
