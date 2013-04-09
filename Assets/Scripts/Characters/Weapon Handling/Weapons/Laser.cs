using UnityEngine;
using System.Collections;

public class Laser : WeaponDelegate {

	public float range = 15f;

	public int damage = 3;

	[SetupableField]
	public LineRenderer trail;

	[SetupableField]
	public ParticleSystem hitEffect;

	[SetupableField]
	public Color startColor;

	[SetupableField]
	public Color endColor;

	public float startWidth = 1f;
	public float endWidth = 1f;

	public override void OnGameReset() { }

	protected override void Shoot() {

		float rangeLeft = range;

		Vector3 rayPosition  = transform.position;
		Vector3 rayDirection = transform.forward;

		Color rayColor = startColor;
		float rayWidth = startWidth;

		while (rangeLeft > 0f) {
			/*
			 This loop should actually never finish on this condition, and always finish on the break later.
			 However, I added this just as a precaution against a neverending loop.
			*/

			RaycastHit hit;			

			Vector3 rayFinish = rayPosition + rayDirection * rangeLeft;

			if (Physics.Linecast ( rayPosition, rayFinish, out hit ) ) {

				// Substracting distance

				rangeLeft -= hit.distance;

				// Instantiating line renderer

				LineRenderer rayRenderer = (LineRenderer) Instantiate ( trail, Vector3.zero, Quaternion.identity );

				rayRenderer.SetVertexCount(2);
				rayRenderer.SetPosition(0, rayPosition);
				rayRenderer.SetPosition(1, hit.point);

				float rayComplete = 1 - (rangeLeft / range);
				
				Color newColor = Color.Lerp(startColor, endColor, rayComplete);
				float newWidth = Mathf.Lerp(startWidth, endWidth, rayComplete);

				rayRenderer.SetColors(rayColor, newColor);
				rayRenderer.SetWidth(rayWidth, newWidth);

				rayColor = newColor;
				rayWidth = newWidth;

				// Instantiating hit effect

				Quaternion hitRotation = new Quaternion();
				hitRotation.SetLookRotation(hit.normal);

				Instantiate(hitEffect, hit.point, hitRotation);

				// Reflecting

				rayPosition = hit.point;
				rayDirection = Vector3.Reflect(rayDirection, hit.normal);

				// If we hit something, inflicting damage

				IDamageReceiver targetDamageReceiver = (IDamageReceiver) hit.collider.GetComponent(typeof(IDamageReceiver));

				if (targetDamageReceiver != null) {

					targetDamageReceiver.InflictDamage(damage);

				}

			} else {

				LineRenderer rayRenderer = (LineRenderer) Instantiate ( trail, Vector3.zero, Quaternion.identity );

				rayRenderer.SetVertexCount(2);
				rayRenderer.SetPosition(0, rayPosition);
				rayRenderer.SetPosition(1, rayFinish);

				rayRenderer.SetColors(rayColor, endColor);
				rayRenderer.SetWidth(rayWidth, endWidth);

				break;

			}

		}

	}

	void OnDrawGizmosSelected() {

		Gizmos.color = Color.blue;

		Gizmos.DrawLine( transform.position, transform.position + transform.forward * 10 );

	}

}
