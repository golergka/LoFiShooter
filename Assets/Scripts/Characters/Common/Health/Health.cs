using UnityEngine;
using System;
using System.Collections;

public interface IDamageReceiver {

	void InflictDamage(int damageAmount);

}

public class Health : BasicBehavior, IDamageReceiver {

	private int _healthPoints;
	public int healthPoints {
		get {
			return _healthPoints;
		}

		private set {

			_healthPoints = value;

			if (OnHealthChange != null) {
				OnHealthChange(this);
			}

		}
	}

	public int maxHealthPoints = 100;

	override public void OnGameReset () {

		healthPoints = maxHealthPoints;
	
	}

	public Action<Health> OnHealthZero;
	public Action<Health> OnHealthChange;
	
	public void InflictDamage(int damageAmount) {

		if ( damageAmount == 0 ) {

			Debug.LogWarning("Received 0 damage!");
			return;

		}

		if ( damageAmount >= healthPoints ) {

			healthPoints = 0;

			gameObject.SetActive(false); // TODO : implement custom death behaviors

			if (OnHealthZero != null) {
				OnHealthZero(this);
			}

		} else {

			healthPoints -= damageAmount;

		}

	}

	public void InflictHealing(int healingAmount) {

		if ( healingAmount == 0 ) {

			Debug.LogWarning("Received 0 healing!");
			return;

		}

		healthPoints = Mathf.Min( healthPoints + healingAmount, maxHealthPoints );

	}

}
