using UnityEngine;
using System.Collections;

public class Shotgun : WeaponDelegate {

	public int pelletDamage = 1;
	public int minPellets = 10;
	public int maxPellets = 15;

	public float spreadAngle = 20f;

	[SetupableField]
	public LineRenderer trail;

	[SetupableField]
	public ParticleSystem hitEffect;

	public override void OnGameReset() { }

	protected override void Shoot () {

		int pellets = Random.Range(minPellets, maxPellets);

		for(int i=0; i<pellets; i++ ) {

			RaycastHit hit;

			Vector3 forward = transform.forward;

			Quaternion spreadRotation = Quaternion.identity;

			Vector3 spreadRotationAngles = new Vector3();
			spreadRotationAngles.y = Random.Range( -spreadAngle, spreadAngle );

			spreadRotation.eulerAngles = spreadRotationAngles;

			forward = spreadRotation * forward;

			if ( Physics.Raycast( transform.position, forward, out hit) ) {

				Health targetHealth = hit.collider.GetComponent<Health>();

				if (targetHealth) {

					targetHealth.InflictDamage(pelletDamage);

				}

				LineRenderer shotLineRenderer = (LineRenderer) Instantiate (trail, Vector3.zero, Quaternion.identity );

				shotLineRenderer.SetVertexCount(2);
				shotLineRenderer.SetPosition(0, transform.position);
				shotLineRenderer.SetPosition(1, hit.point);

				Quaternion hitRotation = new Quaternion();
				hitRotation.SetLookRotation(hit.normal);

				Instantiate(hitEffect, hit.point, hitRotation);

			}

		}

	}
}
