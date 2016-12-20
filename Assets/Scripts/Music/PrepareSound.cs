using UnityEngine;
using System.Collections;

//This class requires other classes to call it.
public class PrepareSound : PreloadAudioInterface {

	private bool typeOfSound;
	private bool loop = true;

	void Awake () {
		loadComps();
		initAudioToComps();
	}

	public override void PlayComp ()
	{
		sound.PlaySound(clip,getCompAudio()[0],typeOfSound);
	}

	public void playOnceOrLoop (bool musicType, bool playCondition, bool playOnce)
	{  
		/*
		 * I want: 
		 * 1) Play only when playCondition true 
		 * 2) If musicType is background, don't loop because it already does. 
		 * 3) If musicType is soundeffect, check to see if it's played once or repeatedly
		 */
		typeOfSound = musicType;
		if (loop && playCondition) {
			//Cover playCondition, 

			PlayComp ();

			if(playOnce || musicType == SoundManager.Background){
				loop = false;
			}

		}

		if(playOnce && !playCondition && musicType== SoundManager.SoundEffect){
			//If we wanted it to play again after playCondition becomes false
			loop = true;
		}
	}
}
