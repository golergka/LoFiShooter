using UnityEngine;
using System.Collections;

public class PlayerMovement : BasicBehavior {

	const string AXIS_HORIZONTAL = "Horizontal";
	const string AXIS_VERTICAL   = "Vertical";

	[ComponentField]
	MovementController movementController;

	public float speed = 1f;
	
	// Update is called once per frame
	void Update () {

		Vector3 motion = new Vector3();
		
		motion.x = Input.GetAxis(AXIS_HORIZONTAL);
		motion.z = Input.GetAxis(AXIS_VERTICAL);

		movementController.speed = speed;
		movementController.Move(motion);

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		if (Physics.Raycast(ray, out hit)) {
			
			Vector3 lookTarget = hit.point;
			lookTarget.y = transform.position.y;

			movementController.LookAt(lookTarget);
			
		}
	
	}
}