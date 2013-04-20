using UnityEngine;
using System.Collections;

public class Dissapearing : BasicBehavior {

	public float timeToDissapear = 1f;
	public string materialColorProperty = "_Color";

	float startTime;

	override public void OnGameReset() {

		startTime = Time.time;

	}

	void AdjustColor(ref Color sourceColor) {

		sourceColor.a = 1f - ( (Time.time - startTime) / timeToDissapear );

	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time - startTime >= timeToDissapear) {

			Destroy(this.gameObject);

		} else {

			if (renderer) {

				Color materialColor = renderer.material.GetColor(materialColorProperty);
				AdjustColor(ref materialColor);
				renderer.material.SetColor(materialColorProperty, materialColor);

			} else if (guiText) {

				Color textColor = guiText.material.color;
				AdjustColor(ref textColor);
				guiText.material.color = textColor;

			}

		}
	
	}

	void OnDestroy() {

		if (renderer) {
			DestroyImmediate(renderer.material);
		} else if (guiText) {
			DestroyImmediate(guiText.material);
		}

	}
}
