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

		iTween.ScaleTo(gameObject, new Hashtable() {
				{ "x", healthPercentage * fullHealthScreenSize },
			});

		guiTexture.color = Color.Lerp( zeroHealthColor, fullHealthColor, healthPercentage);

	}

	void Update() {

		// Maintain that it shows the whole texture and touches the left side of the screen
		transform.position = new Vector3( transform.localScale.x / 2,
			transform.position.y, transform.position.z);

	}

}
