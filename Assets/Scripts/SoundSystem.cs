using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SoundEvent {

	public List<AudioClip> clips;

	public float volumeScale = 1f;

	public void Play(Component owner) {

		AudioSource source = owner.GetComponent<AudioSource>();

		if (!source) {

			Debug.LogWarning("No AudioSource component on the object! Creating my own.");
			source = owner.gameObject.AddComponent<AudioSource>();

		}

		source.PlayOneShot(clips[Random.Range(0, clips.Count - 1)], volumeScale);

	}

}

// This class is actually not needed yet. Created for future use.
public class SoundSystem : MonoBehaviour {

	private static SoundSystem _instance;
	public static SoundSystem instance {

		get {

			if (!_instance) {

				GameObject soundSystemObject = new GameObject("-SoundSystem");
				_instance = soundSystemObject.AddComponent<SoundSystem>();

			}

			return _instance;

		}

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
