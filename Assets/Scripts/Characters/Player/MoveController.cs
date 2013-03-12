using UnityEngine;
using System.Collections;

public class MoveController : MonoBehaviour {

	const string AXIS_HORIZONTAL = "Horizontal";
	const string AXIS_VERTICAL   = "Vertical";

	CharacterController characterController;

	void Awake() {

		characterController = GetComponent<CharacterController>();
		if (!characterController) {
			Debug.LogWarning("No Character Controller found!");
			enabled = false;
		}

	}

	public float speed = 1f;
	
	// Update is called once per frame
	void Update () {

		Vector3 motion = new Vector3();
		
		motion.x = Input.GetAxis(AXIS_HORIZONTAL);
		motion.z = Input.GetAxis(AXIS_VERTICAL);

		motion.Normalize();

		motion *= speed;

		characterController.SimpleMove(motion);
	
	}
}
