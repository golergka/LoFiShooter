using UnityEngine;
using System.Collections;

public class PlayerHealthHUD : BasicBehavior {

	override public void OnGameReset() { }

	const string TAG_PLAYER = "Player";

	public float fullHealthScreenSize = 0.7f;

	[SetupableField]
	public Color fullHealthColor;

	[SetupableField]
	public Color zeroHealthColor;

	// Use this for initialization
	override protected void Start () {

		base.Start();

		GameObject player = GameObject.FindWithTag(TAG_PLAYER);

		if (!player) {
			Debug.LogWarning("Cannot find player by the tag " + TAG_PLAYER);
			return;
		}

		Health playerHealth = player.GetComponent<Health>();

		if (!playerHealth) {
			Debug.LogWarning("Player doesn't have health component!");
			return;
		}

		playerHealth.OnHealthChange += OnHealthChangeHandler;
	
	}
	
	void OnHealthChangeHandler(Health playerHealth) {

		float healthPercentage = (float) playerHealth.healthPoints / (float) playerHealth.maxHealthPoints;

		transform.position = new Vector3( healthPercentage * fullHealthScreenSize / 2,
			transform.position.y, transform.position.z);
		transform.localScale = new Vector3( healthPercentage * fullHealthScreenSize,
			transform.localScale.y, transform.localScale.z);

		guiTexture.color = Color.Lerp( zeroHealthColor, fullHealthColor, healthPercentage);

	}

}
