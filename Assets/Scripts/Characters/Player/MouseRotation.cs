using UnityEngine;
using System.Collections;

public class MouseRotation : BasicBehavior {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		if (Physics.Raycast(ray, out hit)) {
			
			Vector3 lookTarget = hit.point;
			lookTarget.y = transform.position.y;

			transform.LookAt(lookTarget);
			
		}
	
	}
}
