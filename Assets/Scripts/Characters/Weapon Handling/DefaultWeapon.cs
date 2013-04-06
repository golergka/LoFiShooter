using UnityEngine;
using System.Collections;

public class DefaultWeapon : BasicBehavior {

	public WeaponDelegate defaultWeapon;

	[ComponentField]
	WeaponDelegator delegator;

	// Use this for initialization
	void Start () {

		delegator.SwitchWeapon(defaultWeapon);
	
	}
	
}
