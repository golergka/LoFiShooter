using UnityEngine;
using System.Collections;

public class HealthPack : BasicBehavior {

	public int healing;

	public override void OnGameReset() { }

	void OnTriggerEnter(Collider other) {

		Health health = other.GetComponent<Health>();

		if (health) {

			health.InflictHealing(healing);
			Destroy(this.gameObject);

		}

	}
}
