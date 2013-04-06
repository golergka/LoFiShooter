using UnityEngine;
using System.Collections;

public class PlayerWeaponDelegator : WeaponDelegator {

	const string BUTTON_FIRE = "Fire1";
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButton(BUTTON_FIRE))
			Fire();
	
	}

}
