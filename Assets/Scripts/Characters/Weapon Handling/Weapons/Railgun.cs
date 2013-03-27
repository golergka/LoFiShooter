using UnityEngine;
using System.Collections;

public class Railgun : WeaponDelegate {

	public int damage = 1;

	[SetupableField]
	public LineRenderer trail;

	[SetupableField]
	public AnimationCurve spreadOverTime;

	public float spreadReset = 0.2f;

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

		spreadRotationAngles.x = Random.Range( -spreadAngle, spreadAngle );
		spreadRotationAngles.y = Random.Range( -spreadAngle, spreadAngle );
		spreadRotationAngles.z = Random.Range( -spreadAngle, spreadAngle );

		spreadRotation.eulerAngles = spreadRotationAngles;

		forward = spreadRotation * forward;

		if ( Physics.Raycast(transform.position, forward, out hit) ) {

			Health targetHealth = hit.collider.GetComponent<Health>();

			if (targetHealth) {

				targetHealth.InflictDamage(damage);

			}

			LineRenderer shotLineRenderer = (LineRenderer) Instantiate(trail, Vector3.zero, Quaternion.identity);

			// LineRenderer shotLineRenderer = shot.GetComponent<LineRenderer>();

			shotLineRenderer.SetVertexCount(2);
			shotLineRenderer.SetPosition(0, transform.position);
			shotLineRenderer.SetPosition(1, hit.point);

		}

	}

}
