using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ClickSound : PreloadAudioInterface {

	void Awake () {

		loadComps();
		initAudioToComps();
		AudioSource[] temp = getCompAudio();

		for(int i = 0; i< temp.Length; i++){
			temp[i].playOnAwake = false;
		}

	}

	void onMouseOver(){
		PlayComp();
	}

	public override void PlayComp ()
	{
		AudioSource[] tempAudio = getCompAudio();

		for(int i=0; i<tempAudio.Length ;i++){
			sound.PlaySound(clip,tempAudio[i],SoundManager.SoundEffect);
		}
	}


}
