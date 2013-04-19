using UnityEngine;
using System.Collections;

public class CameraController : BasicBehavior {

	public float snapDistance;
	public float smoothSpeed;
	public float maxSpeed = 1f;
	public float snapTimeout;
	public float mouseLookAheadRatio = 0.4f;
	public float mouseLookAheadMax = 4f;

	public float recoilMultiplier = 1f;
	
	Vector3 cameraOffset;
	
	[SetupableField]
	public Transform target;
	
	private float lastSnapTime = 0f;
	
	public static CameraController instance;
	
	protected override void Awake() {

		base.Awake();
		
		instance = this;

		previousTargetPosition = target.position;

		cameraOffset = transform.position - target.position;

		weaponDelegator = target.GetComponent<WeaponDelegator>();
		if (weaponDelegator) {

			weaponDelegator.OnSwitchWeapon += SwitchWeaponHandler;

		}
		
	}

	WeaponDelegate 	weaponDelegate;
	WeaponDelegator weaponDelegator;

	void RecoilHandler(WeaponDelegate d, float recoil) {

		if (d != weaponDelegate) {

			Debug.LogError("Wrong event sender! Expected: " + weaponDelegate + " got: " + d);
			return;

		}

		Vector3 recoilOffset = new Vector3();
		recoilOffset.x = Random.Range(-recoil, recoil);
		recoilOffset.y = Random.Range(-recoil, recoil);
		recoilOffset.z = Random.Range(-recoil, recoil);

		recoilOffset *= recoilMultiplier;

		transform.position += recoilOffset;

	}

	void SwitchWeaponHandler(WeaponDelegator delegator, WeaponDelegate d) {

		if (delegator != weaponDelegator) {

			Debug.LogError("Wrong event sender! Expected: " + weaponDelegator + " got: " + delegator);
			return;

		}

		if (weaponDelegate)
			weaponDelegate.OnRecoil -= RecoilHandler;

		weaponDelegate = d;

		if (weaponDelegate)
			weaponDelegate.OnRecoil += RecoilHandler;

	}

	public override void OnGameReset() { }
	
	private Vector3 targetCamera {
		
		get {
			
			Vector3 mouseLookAhead = PlayerMovement.mouseWorldPosition - target.position;
			mouseLookAhead *= mouseLookAheadRatio;

			if (mouseLookAhead.magnitude > mouseLookAheadMax) {
				
				mouseLookAhead.Normalize();
				mouseLookAhead *= mouseLookAheadMax;

			}

			return (cameraOffset + target.position + mouseLookAhead );
			
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

	Vector3 previousTargetPosition;
	Vector3 targetSpeed;
	
	void LateUpdate() {
		
		targetSpeed = (target.position - previousTargetPosition)/Time.deltaTime;
		targetSpeed.y = 0;

		Apply();
		
		previousTargetPosition = target.position;

	}

}
