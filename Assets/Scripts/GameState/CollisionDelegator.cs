using UnityEngine;
using System.Collections;
using System;

public class CollisionDelegator : BasicBehavior {

	public override void OnGameReset() { }

	public Action<CollisionDelegator, Collider> CollisionEnter;
	public Action<CollisionDelegator, Collider> CollisionStay;
	public Action<CollisionDelegator, Collider> CollisionExit;

	void OnCollisionEnter(Collision collisionInfo) {

		if (CollisionEnter != null) {

			CollisionEnter(this, collisionInfo.collider);

		}

	}

	void OnCollisionStay(Collision collisionInfo) {

		if (CollisionStay != null) {

			CollisionStay(this, collisionInfo.collider);

		}

	}

	void OnCollisionExit(Collision collisionInfo) {

		if (CollisionExit != null) {

			CollisionExit(this, collisionInfo.collider);

		}

	}

	void OnTriggerEnter(Collider other) {

		if (CollisionEnter != null) {

			CollisionEnter(this, other);

		}

	}

	void OnTriggerExit(Collider other) {

		if (CollisionExit != null) {

			CollisionExit(this, other);

		}

	}

	void OnTriggerStay(Collider other) {

		if (CollisionStay != null) {

			CollisionStay(this, other);

		}

	}

}
