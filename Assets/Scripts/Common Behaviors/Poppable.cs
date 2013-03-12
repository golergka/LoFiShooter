using UnityEngine;
using System.Collections;

public class Poppable : MonoBehaviour {

	public int damage;

	void OnCollisionEnter(Collision collision) {

		Health health = collision.gameObject.GetComponent<Health>();

		if (health) {

			health.InflictDamage(damage);

		}

		Debug.Log("Destroy!");

		Destroy(this.gameObject);

	}

}
