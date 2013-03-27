// Please, leave only one movement define uncommented.

using UnityEngine;
using System.Collections;

public class MovementController : BasicBehavior {

	[ComponentField]
	new Rigidbody rigidbody;

	public float speed { get; set; }

	protected override void Awake() {

		base.Awake();
		rigidbody.isKinematic = false;

	}

	public override void OnGameReset() {

		targetVelocity = Vector3.zero;
		rigidbody.velocity = Vector3.zero;

	}

	const float MAX_ACCELERATION = 40f;
	Vector3 targetVelocity;

	// Moving the character in this direction. It's normalized inside.
	public void Move(Vector3 movement) {

		movement.Normalize();
		movement *= speed;

		targetVelocity = movement;

	}

	void FixedUpdate() {

		Vector3 acceleration = (targetVelocity - rigidbody.velocity) / Time.deltaTime;

		acceleration.y = 0;

		Debug.DrawLine(transform.position, transform.position + rigidbody.velocity, Color.green, 0f, false);
		Debug.DrawLine(transform.position, transform.position + targetVelocity, Color.blue, 0f, false);
		Debug.DrawLine(transform.position + rigidbody.velocity, transform.position + targetVelocity, Color.red, 0f, false);

		if (acceleration.magnitude > MAX_ACCELERATION) {

			acceleration.Normalize();
			acceleration *= MAX_ACCELERATION;

		}

		rigidbody.AddForce(acceleration, ForceMode.Acceleration);

	}

	// Can be called every frame, can be called once in a while.
	// Rotates towards the target.
	public void LookAt(Vector3 lookTarget) {

		transform.LookAt(lookTarget);

	}

}
