using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class SoundManager{

	/* Plan: 
	 * *May implement* Make this class a singleton. Delete AudioSource gameObjAudio from the constructor.
	 * 
	 * 3.5) As a result of step 3, every single clip will have a different audiosource. Thus, there could 
	 * be multiple audiosources playing the same sound. *Optional* Ensure the same gameobject has only one of each clip.
	 * 
	 * 4) Create a script replacing soundTest for backgroundMusic.
	 * 
	 * 
	 */
	private static readonly SoundManager instance = new SoundManager();

	private static Dictionary<AudioClip,AudioSource> audio;

	public static bool SoundEffect = true;
	public static bool Background = false;

	static SoundManager(){
		//Explicitly left here for thread-safe C# singleton
		//This will only be called once throughout entire program lifecycle.
		audio = new Dictionary<AudioClip, AudioSource>();
	}
		
	private SoundManager(){
		//Allows for singleton pattern.
	}

	// Public static instance for other classes to use.
	public static SoundManager Instance{
		get{
			return instance;
		}
	}

	public void PlaySound(AudioClip filename,AudioSource tempAudio, bool soundEff){

		if(!audio.ContainsKey(filename)){
			audio.Add(filename,tempAudio);

			var directoryName = "Music/";

			//Returns Sound Effects if true, and returns the other value otherwise
			directoryName += (soundEff) ? "Sound Effects/" : "Music And Ambience/";
				
			directoryName += filename.name;

			tempAudio.clip = Resources.Load(directoryName) as AudioClip;

		}

		PlaySoundType(filename,soundEff);

	}

	private void PlaySoundType(AudioClip filename,bool soundEffect){
		float fullVolume = 1.0f, softVolume = 0.7f;

		if(!audio[filename].isPlaying){
			if(soundEffect){
				audio[filename].PlayOneShot(filename,fullVolume);
				audio[filename].loop = false;
			}else{
				audio[filename].Play();
				audio[filename].volume = softVolume;
				audio[filename].loop = true;
			}
		}
	}

	public void StopSound(){
		foreach(KeyValuePair<AudioClip, AudioSource> tempAudio in audio){
			if(tempAudio.Value.isPlaying)
			{
				tempAudio.Value.Stop();
			}
		}
	}

    public void ClearSounds()
    {
        audio.Clear();
    }

}
