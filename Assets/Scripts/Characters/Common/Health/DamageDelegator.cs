using UnityEngine;
using System.Collections;
using System;

public class DamageDelegator : BasicBehavior, IDamageReceiver {

	public override void OnGameReset() { }

	public Action<DamageDelegator, int> OnDamage;

	public void InflictDamage(int damageAmount) {

		if (OnDamage != null)
			OnDamage(this, damageAmount);

	}

}
