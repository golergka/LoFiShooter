using UnityEngine;
using System.Collections;

public class Poppable : MonoBehaviour {

	public int damage;

	void OnTriggerEnter(Collider other) {

		Health health = other.GetComponent<Health>();

		if (health) {

			health.InflictDamage(damage);

		}

		Destroy(this.gameObject);

	}

}
