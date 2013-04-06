using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SoundEvent {

	public List<AudioClip> clips;

	public float volumeScale = 1f;
	public float randomVolume = 0.1f;

	public void Play(Component owner) {

		AudioSource source = owner.GetComponent<AudioSource>();

		if (!source) {

			Debug.LogWarning("No AudioSource component on the object! Creating my own. Please create AudioSource to configure it properly in the future.");
			source = owner.gameObject.AddComponent<AudioSource>();

		}

		if (clips.Count == 0) {

			Debug.LogWarning("No sound clips configured!");
			return; 
			
		}

		source.PlayOneShot(clips[Random.Range(0, clips.Count - 1)],
			volumeScale + Random.Range(-randomVolume,randomVolume));

	}

}

// This class is actually not needed yet. Created for future use.
public class SoundSystem : BasicBehavior {

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

	public override void OnGameReset() { }
	
}
