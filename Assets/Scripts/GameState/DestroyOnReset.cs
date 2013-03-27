using UnityEngine;
using System.Collections;

public class DestroyOnReset : BasicBehavior {

	bool started = false;

	// Use this for initialization
	protected override void Start () {

		base.Start();
		started = true;
	
	}

	public override void OnGameReset() {

		if (started)
			Destroy(this.gameObject);

	}
	
}
