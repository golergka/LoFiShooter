using UnityEngine;
using System.Collections;

public class Railgun : WeaponDelegate {

	public float firePeriod = 1f;

	public int damage = 1;

	public LineRenderer trail;

	void Awake() {

		if (!trail) {

			Debug.LogWarning("Please configure trail prefab!");
			enabled = false;

		}

	}

	public override void Fire() {

	}

	float lastFireTime = -1;

	public override void FireContinuous() {

		if ( lastFireTime < 0 || (Time.time - lastFireTime) > firePeriod ) {

			lastFireTime = Time.time;

			// TODO : code duplication with BulletGenerator.cs Refactor!

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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
