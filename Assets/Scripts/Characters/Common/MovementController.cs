// Please, leave only one movement define uncommented.

using UnityEngine;
using System.Collections;

public class MovementController : BasicBehavior {

	public float speed { get; set; }

#region Init

	protected override void Awake() {

		base.Awake();
		rigidbody.isKinematic = false;

		if (weaponDelegator) {

			weaponDelegator.OnSwitchWeapon += SwitchWeaponHandler;

		}

	}

	public override void OnGameReset() {

		targetVelocity = Vector3.zero;
		rigidbody.velocity = Vector3.zero;

	}

#endregion

#region Public interface

	// Moving the character in this direction. It's normalized inside.
	public void Move(Vector3 movement) {

		movement.Normalize();
		movement *= speed;

		targetVelocity = movement;

	}

	public void Stop() {

		targetVelocity = Vector3.zero;

	}

	// Can be called every frame, can be called once in a while.
	// Rotates towards the target.
	public void LookAt(Vector3 lookTarget) {

		transform.LookAt(lookTarget);

	}

#endregion

#region Movement

	// I overload default rigidbody property because I need init-time check on it's existance.
	// This check is performed by ComponentField attribute.
	[ComponentField]
	new Rigidbody rigidbody;

	const float MAX_ACCELERATION = 60f;
	Vector3 targetVelocity;

	void FixedUpdate() {

		// It's supposed to be bad practice to use MovePosition on non-kinematic rigidbodies.
		// However, it works just perfectly.
		rigidbody.MovePosition(rigidbody.position + targetVelocity * Time.deltaTime);

	}

#endregion

#region Recoil handling

	/*
	TODO:
	I need to create separate class to handle recoil.
	It should be just an object (not monobehavior) that takes the character who holds the weapon as input
	and a lambda function at contruction time, and then delegates handling the recoil from this character's input to this
	lambda.
	*/

	[ComponentField]
	WeaponDelegator weaponDelegator;

	WeaponDelegate 	weaponDelegate;

	const float recoilMultiplier = 1f;

	void RecoilHandler(WeaponDelegate d, float recoil) {

		if (d != weaponDelegate) {

			Debug.LogError("Wrong event sender! Expected: " + weaponDelegate + " got: " + d);
			return;

		}

		rigidbody.AddForce( (-transform.forward) * recoil * recoilMultiplier, ForceMode.VelocityChange );

	}

	void SwitchWeaponHandler(WeaponDelegator delegator, WeaponDelegate d) {

		if (delegator != weaponDelegator) {

			Debug.LogError("Wrong event sender! Expected: " + weaponDelegator + " got: " + delegator);
			return;

		}

		if (weaponDelegate)
			weaponDelegate.OnRecoil -= RecoilHandler;

		weaponDelegate = d;

		if (weaponDelegate)
			weaponDelegate.OnRecoil += RecoilHandler;

	}

#endregion

}
