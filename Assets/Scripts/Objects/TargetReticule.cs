using UnityEngine;
using System.Collections;

public class TargetReticule : BasicBehavior {

	public override void OnGameReset() { }
	
	// Update is called once per frame
	void Update () {

		Vector3 position = transform.position;

		position.x = Input.mousePosition.x / Screen.width;
		position.y = Input.mousePosition.y / Screen.height;

		transform.position = position;
	
	}
}
