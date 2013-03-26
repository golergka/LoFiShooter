// Please, leave only one movement define uncommented.

// #define MOVEMENT_CC // CharacterController-based movement
#define MOVEMENT_RB // Rgidbody-based movement

using UnityEngine;
using System.Collections;

public class MovementController : BasicBehavior {

	[ComponentField]
	CharacterController characterController;

	[ComponentField]
	new Rigidbody rigidbody;

	public float speed { get; set; }

	protected override void Awake() {

		base.Awake();

#if MOVEMENT_CC

		rigidbody.isKinematic = true;

#endif

#if MOVEMENT_RB

		rigidbody.isKinematic = false;

#endif

	}

#if MOVEMENT_RB

	const float MAX_ACCELERATION = 40f;

	Vector3 targetVelocity;

#endif

	// Moving the character in this direction. It's normalized inside.
	public void Move(Vector3 movement) {

		movement.Normalize();
		movement *= speed;

#if MOVEMENT_CC

		characterController.Move(movement * Time.deltaTime);

#endif

#if MOVEMENT_RB

		targetVelocity = movement;

#endif

	}

#if MOVEMENT_RB

	void FixedUpdate() {

		Vector3 acceleration = (targetVelocity - rigidbody.velocity) / Time.deltaTime;

		Debug.DrawLine(transform.position, transform.position + rigidbody.velocity, Color.green, 0f, false);
		Debug.DrawLine(transform.position, transform.position + targetVelocity, Color.blue, 0f, false);
		Debug.DrawLine(transform.position + rigidbody.velocity, transform.position + targetVelocity, Color.red, 0f, false);

		if (acceleration.magnitude > MAX_ACCELERATION) {

			acceleration.Normalize();
			acceleration *= MAX_ACCELERATION;

		}

		rigidbody.AddForce(acceleration, ForceMode.Acceleration);

	}

#endif

	// Can be called every frame, can be called once in a while.
	// Rotates towards the target.
	public void LookAt(Vector3 lookTarget) {

		transform.LookAt(lookTarget);

	}

}
