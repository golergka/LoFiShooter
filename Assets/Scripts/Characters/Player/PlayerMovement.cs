using UnityEngine;
using System.Collections;

public class PlayerMovement : BasicBehavior {

	const string AXIS_HORIZONTAL    = "Horizontal";
	const string AXIS_VERTICAL      = "Vertical";

	const int    LAYER_CURSOR_PLANE = 9;

	[ComponentField]
	MovementController movementController;

	public float speed = 1f;

	protected override void Awake() {
		base.Awake();

		GameObject cursorPlane = new GameObject("cursorPlane");
		cursorPlane.transform.localScale = new Vector3(100000f,0.00001f,100000f);
		// just big enough numbers that not gonna be used anywhere else, no reason to make them constants
		cursorPlane.transform.position = transform.position;
		cursorPlane.transform.parent = transform;
		cursorPlane.layer = LAYER_CURSOR_PLANE; // CursorPlane
		cursorPlane.AddComponent<BoxCollider>();

	}

	public override void OnGameReset() { }

	// used for other scripts that would need this information
	public static Vector3 mouseWorldPosition { get; private set; }
	
	// Update is called once per frame
	void Update () {

		Vector3 motion = new Vector3();
		
		motion.x = Input.GetAxis(AXIS_HORIZONTAL);
		motion.z = Input.GetAxis(AXIS_VERTICAL);

		movementController.speed = speed;
		movementController.Move(motion);

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		int layerMask = 1 << LAYER_CURSOR_PLANE;
		
		if (Physics.Raycast(ray, out hit, layerMask)) {
			
			Vector3 lookTarget = hit.point;
			lookTarget.y = transform.position.y;
			mouseWorldPosition = hit.point;

			movementController.LookAt(lookTarget);
			
		}
	
	}
}