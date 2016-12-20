using UnityEngine;
using System.Collections;

public class AdjustPlayerOnStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    if(TriggerRoomLoad.movePlayerOnStart)
        {
            GameObject.Find("Player").transform.position = TriggerRoomLoad.relativePlayerPos;
            TriggerRoomLoad.movePlayerOnStart = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
