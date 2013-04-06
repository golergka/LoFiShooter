using UnityEngine;
using System.Collections;

public class PlayerMovement : BasicBehavior {

	const string AXIS_HORIZONTAL = "Horizontal";
	const string AXIS_VERTICAL   = "Vertical";

	[ComponentField]
	CharacterController characterController;

	public float speed = 1f;
	
	// Update is called once per frame
	void Update () {

		Vector3 motion = new Vector3();
		
		motion.x = Input.GetAxis(AXIS_HORIZONTAL);
		motion.z = Input.GetAxis(AXIS_VERTICAL);

		motion.Normalize();

		motion *= speed;

		characterController.Move(motion * Time.deltaTime);
	
	}
}