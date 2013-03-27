using UnityEngine;
using System.Collections;

public class TransformForwardSpeed : BasicBehavior {

	public float speed;

	public override void OnGameReset() { }
	
	// Update is called once per frame
	void Update () {

		transform.position += transform.forward * speed * Time.deltaTime;
	
	}
}
