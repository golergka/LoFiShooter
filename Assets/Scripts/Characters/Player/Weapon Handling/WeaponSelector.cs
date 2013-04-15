using UnityEngine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class WeaponSelector : BasicBehavior {

	// List of weapons currently available to the player.
	// Can be modified in runtime!
	[SetupableField]
	public List<WeaponDelegate> weapons;

	[ComponentField]
	WeaponDelegator delegator;

	int? selectedWeapon = null;

	public int defaultWeapon = 1;

	List<GUITexture> weaponTextures;

	[SetupableField]
	public Rect weaponTextureInset;

	public float rightMargin;
	public float bottomMargin;
	public float space;

	public Color textureColor;
	public Color selectedTextureColor;

	public Vector2 selectedIncrease;

	override public void OnGameReset() {

		weaponTextures = new List<GUITexture>();

		GameObject weaponTextureHolder = new GameObject("Weapon textures");

		foreach(WeaponDelegate weapon in weapons) {

			// Create gameObject
			GameObject weaponTextureObject = new GameObject(weapon.guiName + " gui texture");
			weaponTextureObject.transform.parent = weaponTextureHolder.transform;

			// Create and add GUITexture component
			GUITexture weaponTexture = weaponTextureObject.AddComponent<GUITexture>();
			weaponTextures.Add(weaponTexture);

			// Configure position
			weaponTexture.transform.localScale = Vector3.zero;
			weaponTexture.transform.position = new Vector3(1f,0f,0f);
			
			Rect thisTextureInset = weaponTextureInset;
			thisTextureInset.x -= (weapons.Count - weaponTextures.Count + 1) * (space + weaponTextureInset.width) + rightMargin;
			thisTextureInset.y += bottomMargin;
			weaponTexture.pixelInset = thisTextureInset;

			// Configure GUITexture
			weaponTexture.texture = weapon.guiIcon;
			weaponTexture.color = textureColor;

		}

		SwitchWeapon(defaultWeapon-1);

	}

	const string NUMBERS_ONLY = "/\\D+/g";

	void SwitchWeapon(int newWeaponNumber) {

		if (newWeaponNumber < weapons.Count) {

			delegator.SwitchWeapon(weapons[newWeaponNumber]);

			Rect pixelInset;

			// Old weapon

			if (selectedWeapon != null) {

				weaponTextures[selectedWeapon.Value].color  = textureColor;

				pixelInset = weaponTextures[selectedWeapon.Value].pixelInset;

				pixelInset.width  -= selectedIncrease.x;
				pixelInset.height -= selectedIncrease.y;
				pixelInset.x += selectedIncrease.x/2;
				pixelInset.y += selectedIncrease.y/2;

				weaponTextures[selectedWeapon.Value].pixelInset = pixelInset;

			}

			// New weapon

			weaponTextures[newWeaponNumber].color = selectedTextureColor;

			pixelInset = weaponTextures[newWeaponNumber].pixelInset;

			pixelInset.width  += selectedIncrease.x;
			pixelInset.height += selectedIncrease.y;
			pixelInset.x -= selectedIncrease.x/2;
			pixelInset.y -= selectedIncrease.y/2;

			weaponTextures[newWeaponNumber].pixelInset = pixelInset;

			selectedWeapon = newWeaponNumber;

		}

	}

	const string MOUSE_WHEEL = "Mouse ScrollWheel";

	const float WHEEL_SENSITIVITY = 1f;

	float wheelPosition = 0f;
	
	// Update is called once per frame
	void Update () {

		// Debug.Log("Wheel: " + Input.GetAxis(MOUSE_WHEEL));

		wheelPosition += Input.GetAxis(MOUSE_WHEEL);

		if (Mathf.Abs(wheelPosition) > WHEEL_SENSITIVITY) {

			int sign = (wheelPosition > 0) ? 1 : -1;

			int newWeaponNumber = selectedWeapon.Value + sign;
			
			if (newWeaponNumber < 0)
				newWeaponNumber = weapons.Count - 1;

			if (newWeaponNumber >= weapons.Count)
				newWeaponNumber = 0;

			SwitchWeapon(newWeaponNumber);

			wheelPosition -= WHEEL_SENSITIVITY * sign;
			
		}

		string inputString = Input.inputString;

		Regex rgx = new Regex(NUMBERS_ONLY);

		inputString = rgx.Replace(inputString, "");

		if (inputString != "") {

			int newWeaponNumber = Mathf.RoundToInt((float)char.GetNumericValue(inputString[0]));
			newWeaponNumber--;

			SwitchWeapon(newWeaponNumber);			

		}
	
	}
}
