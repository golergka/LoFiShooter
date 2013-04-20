using UnityEngine;
using System.Collections;

public class DoorController : BasicBehavior {

	[SetupableField]
	public CollisionDelegator collisionDelegator;

	[SetupableField]
	public Transform door;

	public Transform hider;

	public Vector3 openDoorDelta;

	public float animationSpeed = 1f;

	Vector3 closePosition;
	Vector3 openPosition;

	protected override void Awake() {

		base.Awake();

		collisionDelegator.CollisionEnter += CollisionEnterReceiver;
		collisionDelegator.CollisionExit  += CollisionExitReceiver;

		// It's OK if there's no health on the door
		Health doorHealth = door.GetComponent<Health>();

		if (doorHealth)
			doorHealth.OnHealthZero += DoorDestroyedReceiver;

		closePosition = door.position;
		openPosition  = closePosition + openDoorDelta;

	}

#region State

	public enum DoorControllerState {
		Open,
		Closed,
		Destroyed,
	}

	DoorControllerState state;

	void Open() {

		if (state == DoorControllerState.Destroyed)
			return;

		if (hider)
			hider.gameObject.SetActive(false);

		Singleton<ConsoleController>().WriteLine(name + " open");

		state = DoorControllerState.Open;
		iTween.MoveTo(door.gameObject, openPosition, animationSpeed);

	}

	void Close() {

		if (state == DoorControllerState.Destroyed)
			return;

		Singleton<ConsoleController>().WriteLine(name + " close");

		state = DoorControllerState.Closed;
		iTween.MoveTo(door.gameObject, closePosition, animationSpeed);

	}

#endregion

	int _actorsInside = 0;
	int actorsInside {
		get { return _actorsInside; }
		set {

			if (value == 0 && actorsInside > 0) {

				Close();

			} else if (value > 0 && actorsInside == 0) {

				Open();

			}
			
			_actorsInside = value;

		}
	}

	public override void OnGameReset() {

		door.gameObject.SetActive(true);
		state = DoorControllerState.Closed;
		hider.gameObject.SetActive(true);

	}

#region Collision handlers
	
	void CollisionEnterReceiver(CollisionDelegator collisionDelegator, Collider collider) {

		if (collisionDelegator != this.collisionDelegator) {
			Debug.LogError("Got wrong collision delegator! Expected: " + this.collisionDelegator +
				" got: " + collisionDelegator);
			return;
		}

		actorsInside++;

	}

	void CollisionExitReceiver(CollisionDelegator collisionDelegator, Collider collder) {

		if (collisionDelegator != this.collisionDelegator) {
			Debug.LogError("Got wrong collision delegator! Expected: " + this.collisionDelegator +
				" got: " + collisionDelegator);
			return;
		}

		actorsInside--;

	}

#endregion

#region Door health handlers

	void DoorDestroyedReceiver(Health door) {

		if (door != this.door) {
			Debug.LogError("Got wrong door! Expected: " + this.door +
				" got: " + door );
			return;
		}

		state = DoorControllerState.Destroyed;

	}

#endregion

}