using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int healthPoints { get; private set; }

	public int maxHealthPoints = 100;

	public bool showHUD = true;

	[HideInInspector]
	public HealthHUD hud;

	// Use this for initialization
	void Start () {

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

}
