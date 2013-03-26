using UnityEngine;
using System.Collections;

public class EnemyController : BasicBehavior, IVisionListener {

	[ComponentField]
	Vision vision;
	
	[ComponentField]
	CharacterController characterController;

	[ComponentField]
	WeaponDelegator weaponDelegator;

	const string TAG_PLAYER = "Player"; // TODO: Create separate static class with all game-level constants

	enum EnemyState {
		Idle,
		Engage,
	}

	EnemyState state = EnemyState.Idle;

	Transform target;

	public float speed;
	public float attackRange;

	void Idle() {

		// TODO: random small movements around

	}

	void Engage() {

		Vector3 motion = target.position - transform.position;
		motion.y = 0; // we move on horizontal plane;
		
		// This code duplicates PlayerMovement.cs
		// TODO: research refactoring probability without being insane architecture astronaut

		motion.Normalize();
		motion *= speed;
		characterController.Move(motion * Time.deltaTime);

		// And this as MouseRotation.cs
		// TODO: same

		Vector3 lookTarget = target.position;
		lookTarget.y = transform.position.y;
		transform.LookAt(lookTarget);

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
