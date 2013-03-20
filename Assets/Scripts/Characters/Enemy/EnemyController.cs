using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour, IVisionListener {

	Vision 				vision;
	CharacterController characterController;
	WeaponDelegator		weaponDelegator;

	void Awake() {

		// TODO: THIS MESS SHOULD BE SORTED OUT WITH STANDARD MONOBEHAVIOR SOON!

		vision 				= GetComponent<Vision>();
		characterController = GetComponent<CharacterController>();
		weaponDelegator 	= GetComponent<WeaponDelegator>();

		if (!vision) {

			Debug.LogWarning("Please add vision!");
			enabled = false;

		}

		if (!characterController) {

			Debug.LogWarning("Please add characterController!");
			enabled = false;

		}

		if (!weaponDelegator) {

			Debug.LogWarning("Please add weaponDelegator!");
			enabled = false;

		}

	}

	const string TAG_PLAYER = "Player";

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

		if ( (target.position - transform.position).magnitude < attackRange ) {

			weaponDelegator.FireContinuous();
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
