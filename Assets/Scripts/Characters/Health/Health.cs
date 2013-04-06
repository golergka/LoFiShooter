using UnityEngine;
using System;
using System.Collections;

public interface IDamageReceiver {

	void InflictDamage(int damageAmount);

}

public class Health : BasicBehavior, IDamageReceiver {

	public int healthPoints { get; private set; }

	public int maxHealthPoints = 100;

	public bool showHUD = true;

	[HideInInspector]
	public HealthHUD hud;

	override public void OnGameReset () {

		healthPoints = maxHealthPoints;
	
	}

	public Action<Health> OnHealthZero;
	
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
