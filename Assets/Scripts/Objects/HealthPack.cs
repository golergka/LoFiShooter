using UnityEngine;
using System.Collections;

public class HealthPack : BasicBehavior {

	public int healing;

	void OnTriggerEnter(Collider other) {

		Health health = other.GetComponent<Health>();

		if (health) {

			health.InflictHealing(healing);
			Destroy(this.gameObject);

		}

	}
}
