using UnityEngine;
using System.Collections;

public class DefaultWeapon : BasicBehavior {

	[SetupableField]
	public WeaponDelegate defaultWeapon;

	[ComponentField]
	WeaponDelegator delegator;

	public override void OnGameReset () {

		delegator.SwitchWeapon(defaultWeapon);
	
	}
	
}
