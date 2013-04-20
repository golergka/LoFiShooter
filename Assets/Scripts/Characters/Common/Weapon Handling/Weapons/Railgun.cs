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

		// Preparing the spread
		float spreadAngle = spreadOverTime.Evaluate(Time.time - firstShot);
		Quaternion spreadRotation = Quaternion.identity;
		Vector3 spreadRotationAngles = new Vector3();
		spreadRotationAngles.y = Random.Range( -spreadAngle, spreadAngle );
		spreadRotation.eulerAngles = spreadRotationAngles;

		lastShot = Time.time;

		// Preparing the hit
		RaycastHit hit;
		Vector3 forward = transform.forward;
		forward = spreadRotation * forward;

		if ( Physics.Raycast(transform.position, forward, out hit) ) {

			Hit(hit.collider.gameObject, hit.point, forward, hit.normal, damage);

			// Render line

			LineRenderer shotLineRenderer = (LineRenderer) Instantiate(trail, Vector3.zero, Quaternion.identity);

			shotLineRenderer.SetVertexCount(2);
			shotLineRenderer.SetPosition(0, transform.position);
			shotLineRenderer.SetPosition(1, hit.point);

		}

	}

}
