using UnityEngine;
using System.Collections;

public class AddSoundToTrigger : PreloadAudioInterface {

	void Awake () {
		loadComps();
		initAudioToComps();

		if(gameObject.GetComponent<BoxCollider>() == null){
			gameObject.AddComponent<BoxCollider>();
			gameObject.GetComponent<BoxCollider>().isTrigger = true;
		}

		if(gameObject.GetComponent<Rigidbody>() == null){
			gameObject.AddComponent<Rigidbody>();
			gameObject.GetComponent<Rigidbody>().isKinematic = true;
		}
	}

	void OnTriggerEnter(Collider other){

		if(other.gameObject.name == "Player"){
			PlayComp();
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
