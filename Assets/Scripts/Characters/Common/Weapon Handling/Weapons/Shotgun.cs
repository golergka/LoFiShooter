using UnityEngine;
using System.Collections;

public class Shotgun : WeaponDelegate {

	public int pelletDamage = 1;
	public int minPellets = 10;
	public int maxPellets = 15;

	public float minPelletRange = 5f;
	public float maxPelletRange = 10f;

	public float spreadAngle = 20f;

	[SetupableField]
	public LineRenderer trail;

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

			float pelletRange = Random.Range(minPelletRange, maxPelletRange);
			Vector3 pelletLineFinish = transform.position + forward * pelletRange;

			if ( Physics.Linecast( transform.position, pelletLineFinish, out hit) ) {

				Hit(hit.collider.gameObject, hit.point, forward, hit.normal, pelletDamage);

				pelletLineFinish = hit.point;

			}

			LineRenderer shotLineRenderer = (LineRenderer) Instantiate (trail, Vector3.zero, Quaternion.identity );

			shotLineRenderer.SetVertexCount(2);
			shotLineRenderer.SetPosition(0, transform.position);
			shotLineRenderer.SetPosition(1, pelletLineFinish );						

		}

	}
}
