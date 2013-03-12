using UnityEngine;
using System.Collections;

public class DefaultWeapon : MonoBehaviour {

	public WeaponDelegate defaultWeapon;

	// Use this for initialization
	void Start () {

		WeaponDelegator delegator = GetComponent<WeaponDelegator>();

		if (!delegator) {

			Debug.LogWarning("No delegator found!");
			return;

		}

		delegator.SwitchWeapon(defaultWeapon);
	
	}
	
}
