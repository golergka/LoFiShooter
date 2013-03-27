using UnityEngine;
using System.Collections;

public class Health : BasicBehavior {

	public int healthPoints { get; private set; }

	public int maxHealthPoints = 100;

	public bool showHUD = true;

	[HideInInspector]
	public HealthHUD hud;

	override public void OnGameReset () {

		healthPoints = maxHealthPoints;
	
	}
	
	public void InflictDamage(int damageAmount) {

		if ( damageAmount == 0 ) {

			Debug.LogWarning("Received 0 damage!");
			return;

		}

		if ( damageAmount >= healthPoints ) {

			healthPoints = 0;
			gameObject.SetActive(false); // TODO : implement custom death behaviors

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
