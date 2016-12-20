using UnityEngine;
using System.Collections;

public abstract class PreloadAudioInterface : MonoBehaviour {

	private GameObject[] gameObjects;
	public GameObject[] allTheObj = new GameObject[1];

	public AudioClip clip;
	protected SoundManager sound = SoundManager.Instance;

	public void loadComps ()
	{
		int size = 0;
		int count = 1;

		for(int i =0; i< allTheObj.Length; i++){
			if(allTheObj[i]!=null){ size++; }
		}

		gameObjects = new GameObject[size + 1];
		gameObjects[0] = gameObject;

		for(int i = 0; i<allTheObj.Length; i++){
			if(allTheObj[i] != null){ 
				gameObjects[count] = allTheObj[i];
				count++;
			}
		}

	}

	public void initAudioToComps(){
		for(int i = 0; i<gameObjects.Length; i++){
			gameObjects[i].AddComponent<AudioSource>();
		}
	}

	public AudioSource[] getCompAudio(){
		AudioSource[] tempAudio = new AudioSource[gameObjects.Length];

		for(int i=0; i<gameObjects.Length ;i++){
			tempAudio[i] = gameObjects[i].GetComponent<AudioSource>() as AudioSource;
		}

		return tempAudio;
	}

	public abstract void PlayComp();
}
