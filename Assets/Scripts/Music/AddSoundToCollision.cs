using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AddSoundToCollision : PreloadAudioInterface {
	bool playMus = false;

	public bool playMusicID{
		get{
			return playMus;
		}

		set{
			playMus = value;
		}
	}

	void Awake () {

		loadComps();
		initAudioToComps();

		if(gameObject.GetComponent<Rigidbody>() == null){
			gameObject.AddComponent<Rigidbody>();
			gameObject.GetComponent<Rigidbody>().isKinematic = true;
		}
	}

	void OnCollisionEnter(Collision other){

		if(playMus && other.gameObject.name != "Player"){
			PlayComp();
			playMus = false;
		}
	}

	public override void PlayComp ()
	{
		AudioSource[] tempAudio = getCompAudio();

		for(int i=0; i<tempAudio.Length ;i++){
			sound.PlaySound(clip,tempAudio[i],SoundManager.SoundEffect);
		}
	}




}
