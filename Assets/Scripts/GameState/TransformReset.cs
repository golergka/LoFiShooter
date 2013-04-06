using UnityEngine;
using System.Collections;

public class TransformReset : BasicBehavior {

	Vector3    position;
	Quaternion rotation;
	bool	   activeSelf;

	protected override void Awake () {

		base.Awake();

		position   = transform.localPosition;
		rotation   = transform.localRotation;
		activeSelf = gameObject.activeSelf;
	
	}

	override public void OnGameReset() {

		gameObject.SetActive(activeSelf);
		transform.localPosition = position;
		transform.localRotation = rotation;

	}
	
	
}
