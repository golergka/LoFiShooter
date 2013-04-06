using UnityEngine;
using System.Collections;

public class Dissapearing : BasicBehavior {

	public float timeToDissapear = 1f;
	public string materialColorProperty = "_Color";

	float startTime;

	override public void OnGameReset() {

		startTime = Time.time;

	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time - startTime >= timeToDissapear) {

			Destroy(this.gameObject);

		} else {

			Color materialColor = renderer.material.GetColor(materialColorProperty);
			materialColor.a = 1f - ( (Time.time - startTime) / timeToDissapear );
			renderer.material.SetColor(materialColorProperty, materialColor);

		}
	
	}

	void OnDestroy() {

		DestroyImmediate(renderer.material);

	}
}
