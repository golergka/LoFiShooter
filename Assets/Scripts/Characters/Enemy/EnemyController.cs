using UnityEngine;
using System.Collections;

public class EnemyController : BasicBehavior, IVisionListener {

	[ComponentField]
	Vision vision;
	
	[ComponentField]
	MovementController movementController;

	[ComponentField]
	WeaponDelegator weaponDelegator;

	public Light aiLight;

	public Color idleColor;
	public Color engageColor;

	const string TAG_PLAYER = "Player"; // TODO: Create separate static class with all game-level constants

	enum EnemyState {
		Idle,
		Engage,
	}

	EnemyState state;

	override public void OnGameReset() {

		state = EnemyState.Idle;
		target = null;
		if (aiLight) {
			aiLight.color = idleColor;
		}

	}

	Transform target;
	
	public float speed;
	public float attackRange;
	public float maxRange; // maximum range towards target

	void Idle() {

		// TODO: random small movements around

		movementController.Move(Vector3.zero);
		movementController.LookAt(transform.forward);

	}

	void Engage() {

		if ( (transform.position - target.position).magnitude > maxRange ) {

			movementController.speed = speed;
			movementController.Move( target.position - transform.position );

		} else {

			movementController.Stop();

		}

		movementController.LookAt( target.position );

		if ( weaponDelegator && (target.position - transform.position).magnitude < attackRange ) {

			Debug.DrawLine(transform.position, target.position);
			weaponDelegator.Fire();
		}

	}

	public void OnNoticed(Visible observee) {

		if (state == EnemyState.Idle && observee.gameObject.tag == TAG_PLAYER) {

			target = observee.transform;
			state = EnemyState.Engage;
			if (aiLight) {
				aiLight.color = engageColor;
			}

		}

	}

	public void OnLost(Visible observee) {

		if (state == EnemyState.Engage && observee.gameObject.tag == TAG_PLAYER) {

			foreach( Visible v in vision.VisiblesInSight() ) {

				if ( v.gameObject.tag == TAG_PLAYER ) {

					target = v.transform;
					return;

				}

			}

			target = null;
			state = EnemyState.Idle;
			if (aiLight) {
				aiLight.color = idleColor;
			}

		}

	}
	
	// Update is called once per frame
	void Update () {

		switch(state) {

			case EnemyState.Idle:
				Idle();
				break;

			case EnemyState.Engage:
				Engage();
				break;

			default:
				Debug.LogError("Unknown enemy state!");
				break;

		}
	
	}
}
