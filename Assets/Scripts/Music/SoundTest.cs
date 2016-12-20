using UnityEngine;
using System.Collections;

public class SoundTest : MonoBehaviour{

	SoundManager sound;
	AudioSource thisAudio;
	public AudioClip clip;
	public AudioClip clip2;

	void Start () {
		sound = SoundManager.Instance;
		thisAudio = gameObject.GetComponent<AudioSource>();
	}

	void Update(){

		if(Input.GetKeyDown(KeyCode.Z)){
			sound.PlaySound(clip, thisAudio,false);
		}else if(Input.GetKeyDown(KeyCode.X)){
			sound.StopSound();
		}
		else if(Input.GetKeyDown(KeyCode.Y)){
			sound.PlaySound(clip2, thisAudio,false);
		}
	}


}
