using UnityEngine;
using System.Collections;

public class Laser : WeaponDelegate {

	public float range = 15f;

	public int damage = 3;

	[SetupableField]
	public LineRenderer trail;

	[SetupableField]
	public Color startColor;

	[SetupableField]
	public Color endColor;

	public float startWidth = 1f;
	public float endWidth = 1f;

	public override void OnGameReset() { }

	void RenderLine(Vector3 start, Vector3 end, Color startColor, Color endColor, float startWidth, float endWidth) {

		LineRenderer rayRenderer = (LineRenderer) Instantiate ( trail, Vector3.zero, Quaternion.identity );

		rayRenderer.SetVertexCount(2);
		rayRenderer.SetPosition(0, start);
		rayRenderer.SetPosition(1, end);

		rayRenderer.SetColors(startColor, endColor);
		rayRenderer.SetWidth(startWidth, endWidth);

	}

	protected override void Shoot() {

		float rangeLeft = range;

		Vector3 rayPosition  = transform.position;
		Vector3 rayDirection = transform.forward;

		Color rayColor = startColor;
		float rayWidth = startWidth;

		RaycastHit hit;

		// ATTENTION!
		// This line repeats below at the end of the loop.
		Vector3 rayFinish = rayPosition + rayDirection * rangeLeft;

		while (Physics.Linecast ( rayPosition, rayFinish, out hit ) ) {

			// Substracting distance

			rangeLeft -= hit.distance;

			// Instantiating line renderer

			float rayComplete = 1 - (rangeLeft / range);
			
			Color newColor = Color.Lerp(startColor, endColor, rayComplete);
			float newWidth = Mathf.Lerp(startWidth, endWidth, rayComplete);

			RenderLine(rayPosition, hit.point, rayColor, newColor, rayWidth, newWidth);

			rayColor = newColor;
			rayWidth = newWidth;

			// Hitting it

			Hit(hit.collider.gameObject, hit.point, (rayFinish - rayDirection), hit.normal, damage);

			// Reflecting

			rayPosition = hit.point;
			rayDirection = Vector3.Reflect(rayDirection, hit.normal);

			// ATTENTION!
			// This is the same line as the line above, before the loop.
			rayFinish = rayPosition + rayDirection * rangeLeft;

		}

		RenderLine(rayPosition, rayFinish, rayColor, endColor, rayWidth, endWidth);

	}

	void OnDrawGizmosSelected() {

		Gizmos.color = Color.blue;

		Gizmos.DrawLine( transform.position, transform.position + transform.forward * 10 );

	}

}
