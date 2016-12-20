using UnityEngine;
using System.Collections;

public class ButtonToActivate : MonoBehaviour {

	public GameObject activator;
	public GameObject toActivate;
	public bool makeAppear = true;
	private MeshRenderer clicker;

	// Use this for initialization
	void Start () {
		toActivate.SetActive(!makeAppear);
		clicker = gameObject.GetComponent<MeshRenderer>();
	}

	//When makeAppear is true, the following will cause toActivate to appear when button is hit.
	//When makeAppear is false, the following will cause toActivate to disappear when button is hit.
	void OnTriggerEnter(Collider collidedWith)
	{
		if(collidedWith.name == activator.name){
			clicker.enabled = false;
			toActivate.SetActive(makeAppear);
		}
	}

	void OnTriggerExit(Collider collidedWith)
	{
		if(collidedWith.name == activator.name){
			clicker.enabled = true;
			toActivate.SetActive(!makeAppear);
		}
	}

}
