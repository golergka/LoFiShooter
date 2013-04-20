using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConsoleController : BasicBehavior {

	Queue<GUIText> lines = new Queue<GUIText>();

	[SetupableField]
	public GUIText linePrototype;

	public int maxLines = 10;

	public float scrollTime = 1f;

	public void WriteLine(string message) {

		// Add new line

		GUIText newLine = (GUIText) Instantiate(linePrototype, Vector3.zero, Quaternion.identity);
		newLine.text = message;
		lines.Enqueue(newLine);

		// Remove empty lines
		while(lines.Peek() == null) {
			lines.Dequeue();
		}

		// Remove oldest line if we're over the limit

		if (lines.Count > maxLines) {

			GUIText oldLine = lines.Dequeue();
			Destroy(oldLine);

		}

		// Move lines down

		int lineNumber = lines.Count;
		foreach(GUIText line in lines) {

			iTween.MoveTo(line.gameObject, new Hashtable() {
					{ "y", (float) newLine.fontSize * lineNumber / (float) Screen.height },
					{ "time", scrollTime},
				});

			lineNumber--;

		}

	}

	public override void OnGameReset() {

		lines.Clear();

	}

}
