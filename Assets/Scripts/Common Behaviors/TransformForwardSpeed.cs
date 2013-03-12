using UnityEngine;
using System.Collections;

public class TransformForwardSpeed : MonoBehaviour {

	public float speed;
	
	// Update is called once per frame
	void Update () {

		transform.position += transform.forward * speed * Time.deltaTime;
	
	}
}
