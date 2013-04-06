using UnityEngine;
using System.Collections;

public class Poppable : BasicBehavior {

	public int damage;

	public bool requireHealthToPop = false;

	void OnTriggerEnter(Collider other) {

		if (other.gameObject == this.gameObject)
			return;

		Health health = other.GetComponent<Health>();

		if (health) {

			health.InflictDamage(damage);
			Destroy(this.gameObject);
			return;

		}

		if (!requireHealthToPop)
			Destroy(this.gameObject);

	}

	void OnControllerColliderHit(ControllerColliderHit hit) {

		OnTriggerEnter(hit.collider);

	}

}
