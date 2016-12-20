using UnityEngine;
using System.Collections;

public class BackgroundMusic : PreloadAudioInterface {

	void Awake () {
		loadComps();
		initAudioToComps();

		PlayComp();
	}

	public override void PlayComp ()
	{
		sound.PlaySound(clip,getCompAudio()[0],SoundManager.Background);
	}
	

}
