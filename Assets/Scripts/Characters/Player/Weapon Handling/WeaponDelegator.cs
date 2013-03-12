using UnityEngine;
using System.Collections;

public abstract class WeaponDelegate : MonoBehaviour {

	public abstract void Fire();

	public abstract void FireContinuous();

}

public class WeaponDelegator : MonoBehaviour {

	WeaponDelegate weaponDelegate;

	const string BUTTON_FIRE = "Fire1";

	public Transform weaponMount;

	void Awake() {

		if (!weaponMount) {

			Debug.LogWarning("Please, configure weapon mount!");
			enabled = false;

		}

	}

	public void SwitchWeapon(WeaponDelegate weaponDelegatePrefab) {

		Destroy(weaponDelegate);

		if (weaponDelegatePrefab == null)
			return;

		weaponDelegate = Instantiate(weaponDelegatePrefab, weaponMount.position, weaponMount.rotation) as WeaponDelegate;
		weaponDelegate.transform.parent = weaponMount;

	}
	
	// Update is called once per frame
	void Update () {

		if (weaponDelegate == null)
			return;

		if (Input.GetButtonDown(BUTTON_FIRE))
			weaponDelegate.Fire();

		if (Input.GetButton(BUTTON_FIRE))
			weaponDelegate.FireContinuous();
	
	}
}
