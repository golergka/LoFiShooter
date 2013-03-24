using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Vision))]
public class VisionEditor : Editor {

	Vision vision;

	void OnEnable() {

		vision = (Vision) target;

	}

	public override void OnInspectorGUI() {

		if (Application.isPlaying) {

			EditorGUILayout.LabelField("Vision Distance:", vision.visionDistance.ToString() );

		} else {
			
			if ( VisibleGrid.instance == null ) {
				EditorGUILayout.LabelField("Please, create VisibleGrid object.");
				return;
			}

			vision.visionDistanceEditor = EditorGUILayout.Slider(vision.visionDistanceEditor, 0, VisibleGrid.instance.gridStep);

		}
		
	}

}
