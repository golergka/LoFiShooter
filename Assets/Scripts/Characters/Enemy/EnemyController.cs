using UnityEngine;
using System.Collections;

public class EnemyController : BasicBehavior, IVisionListener {

	[ComponentField]
	Vision vision;
	
	[ComponentField]
	MovementController movementController;

	[ComponentField]
	WeaponDelegator weaponDelegator;

	const string TAG_PLAYER = "Player"; // TODO: Create separate static class with all game-level constants

	enum EnemyState {
		Idle,
		Engage,
	}

	EnemyState state;

	override public void OnGameReset() {

		state = EnemyState.Idle;
		target = null;

	}

	Transform target;
	
	public float speed;
	public float attackRange;

	void Idle() {

		// TODO: random small movements around

		movementController.Move(Vector3.zero);
		movementController.LookAt(transform.forward);

	}

	void Engage() {

		movementController.speed = speed;
		movementController.Move( target.position - transform.position );
		movementController.LookAt( target.position );

		if ( weaponDelegator && (target.position - transform.position).magnitude < attackRange ) {

			weaponDelegator.Fire();
		}

	}

	public void OnNoticed(Visible observee) {

		if (state == EnemyState.Idle && observee.gameObject.tag == TAG_PLAYER) {

			target = observee.transform;
			state = EnemyState.Engage;

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
