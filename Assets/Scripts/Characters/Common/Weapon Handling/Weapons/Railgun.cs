using UnityEngine;
using System.Collections;

public class Railgun : WeaponDelegate {

	public int damage = 1;

	[SetupableField]
	public LineRenderer trail;

	[SetupableField]
	public AnimationCurve spreadOverTime;

	[SetupableField]
	public ParticleSystem hitEffect;

	public float spreadReset = 0.2f;

	public float pushForce = 1f;

	float firstShot = -1000f; // just a big enough value to keep the code simple
	float lastShot = -1000f;

	public override void OnGameReset() { }

	protected override void Shoot() {

		// Resetting the spread if it's due
		if ( (Time.time - lastShot) > spreadReset )
			firstShot = Time.time;

		lastShot = Time.time;
	
		RaycastHit hit;

		Vector3 forward = transform.forward;

		float spreadAngle = spreadOverTime.Evaluate(Time.time - firstShot);

		Quaternion spreadRotation = Quaternion.identity;
		Vector3 spreadRotationAngles = new Vector3();

		spreadRotationAngles.y = Random.Range( -spreadAngle, spreadAngle );

		spreadRotation.eulerAngles = spreadRotationAngles;

		forward = spreadRotation * forward;

		if ( Physics.Raycast(transform.position, forward, out hit) ) {

			// Deliver damage

			IDamageReceiver targetDamageReceiver = (IDamageReceiver) hit.collider.GetComponent(typeof(IDamageReceiver));

			if (targetDamageReceiver != null) {

				targetDamageReceiver.InflictDamage(damage);

			}

			// Apply push force

			Rigidbody targetRigidbody = hit.collider.GetComponent<Rigidbody>();

			if (targetRigidbody) {

				Vector3 pushForceVector = forward;
				pushForceVector.Normalize();
				pushForceVector *= pushForce;

				targetRigidbody.AddForce(pushForceVector);

			}

			// Render line

			LineRenderer shotLineRenderer = (LineRenderer) Instantiate(trail, Vector3.zero, Quaternion.identity);

			shotLineRenderer.SetVertexCount(2);
			shotLineRenderer.SetPosition(0, transform.position);
			shotLineRenderer.SetPosition(1, hit.point);

			// Instantiate effect

			Quaternion hitRotation = new Quaternion();
			hitRotation.SetLookRotation(hit.normal);

			Instantiate(hitEffect, hit.point, hitRotation);

		}

	}

}
