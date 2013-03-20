using UnityEngine;
using System.Collections;

public class PlayerWeaponDelegator : WeaponDelegator {

	const string BUTTON_FIRE = "Fire1";
	
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
