using UnityEngine;
using System.Collections;

public class Poppable : BasicBehavior {

	public int damage;

	public bool requireHealthToPop = false;

	public override void OnGameReset() { }

	void OnTriggerEnter(Collider other) {

		if (other.gameObject == this.gameObject)
			return;

		IDamageReceiver damageReceiver = (IDamageReceiver) other.GetComponent(typeof(IDamageReceiver));

		if (damageReceiver != null) {

			damageReceiver.InflictDamage(damage);
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
