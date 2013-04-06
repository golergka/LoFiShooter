using UnityEngine;
using System.Collections;

public class ParticleEffectDestroyer : BasicBehavior {

	[ComponentField]
	new ParticleSystem particleSystem;
	
	// Update is called once per frame
	void Update () {

		if ( !particleSystem.IsAlive() )
			Destroy(this.gameObject);
	
	}

	override public void OnGameReset() { }

}
