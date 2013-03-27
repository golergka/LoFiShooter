using UnityEngine;
using System.Collections.Generic;

public class GameManager : BasicBehavior {

	protected override void Start() {

		base.Start();

		foreach(Object go in FindObjectsOfType(typeof(GameObject)) ) {

			GameObject gObject = ( (GameObject) go );

			if (!gObject.isStatic)
				gObject.AddComponent<TransformReset>();

		}

	}

	public override void OnGameReset() { }

	const string BUTTON_RESET = "Reset";

	void Update() {

		// TODO: Solve problems with input!
		if( Input.inputString.ToLowerInvariant().Contains("r") ) {

			List<BasicBehavior> toReset = new List<BasicBehavior>();

			foreach(BasicBehavior bb in behaviors) {

				toReset.Add(bb);

			}

			foreach(BasicBehavior bb in toReset ) {

				bb.OnGameReset();

			}

		}

	}

}
