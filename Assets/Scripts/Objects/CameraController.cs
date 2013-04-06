using UnityEngine;
using System.Collections;

public class CameraController : BasicBehavior {

	public float snapDistance;
	public float smoothSpeed;
	public float maxSpeed = 1f;
	public float snapTimeout;
	public float lookAhead = 1f;
	
	Vector3 cameraOffset;
	
	[SetupableField]
	public Transform target;
	
	private float lastSnapTime = 0f;
	
	public static CameraController instance;
	
	protected override void Awake() {

		base.Awake();
		
		instance = this;

		cameraOffset = transform.position - target.position;
		
	}
	
	private Vector3 targetCamera {
		
		get {
			
			return (cameraOffset + target.position);
			
		}
		
	}
	
	public void Snap() {
		
		transform.position = targetCamera;
		lastSnapTime = Time.time;
		
	}
	
	public void Apply() {
		
		if ( Vector3.Distance(transform.position, targetCamera ) < snapDistance &&
			Time.time - lastSnapTime > snapTimeout ) {
			
			Snap ();
			
		} else {
			
			Vector3 movement = targetCamera - transform.position;
			movement *= Mathf.Min(maxSpeed * Time.deltaTime, movement.magnitude * smoothSpeed * Time.deltaTime);
			transform.position += movement;
			
		}
		
	}
	
	void LateUpdate() {
		
		Apply();
		
	}

}
