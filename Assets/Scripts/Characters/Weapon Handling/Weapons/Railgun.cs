using UnityEngine;
using System.Collections;

public class Railgun : WeaponDelegate {

	public int damage = 1;

	[SetupableField]
	public LineRenderer trail;

	public override void OnGameReset() { }

	protected override void Shoot() {
	
		RaycastHit hit;

		if ( Physics.Raycast(transform.position, transform.forward, out hit) ) {

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
