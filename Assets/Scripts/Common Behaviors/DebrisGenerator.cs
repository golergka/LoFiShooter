using UnityEngine;
using System.Collections;

public class DebrisGenerator : BasicBehavior {

	[ComponentField]
	Health health;

	[SetupableField]
	public Transform debris;

	// Use this for initialization
	override protected void Start () {

		base.Start();

		health.OnHealthZero += HealthZeroHandler;
	
	}

	void HealthZeroHandler(Health health) {

		Instantiate(debris, transform.position, transform.rotation);

	}

	public override void OnGameReset() { }

}
