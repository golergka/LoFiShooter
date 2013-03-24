using UnityEngine;
using System.Collections;

public class TargetReticule : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 position = transform.position;

		position.x = Input.mousePosition.x / Screen.width;
		position.y = Input.mousePosition.y / Screen.height;

		transform.position = position;
	
	}
}
