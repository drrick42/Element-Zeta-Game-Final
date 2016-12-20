using UnityEngine;
using System.Collections;

public class TimerTrigger : MonoBehaviour {

    public float triggerTime;
    private bool entered;

    public GameObject trigger, reqObject;

	// Use this for initialization
	void Start () {
        entered = false;
        trigger.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	    if (entered)
        {
            if (triggerTime > 0) triggerTime = triggerTime - Time.deltaTime;
        }
        if (triggerTime <= 0)
        {
            trigger.SetActive(true);
            gameObject.SetActive(false);
        }
	}

    void OnTriggerEnter(Collider collidedWith)
    {
        if (collidedWith.gameObject == reqObject.gameObject)
        {
            entered = true;
        }
    }

    void OnTriggerExit(Collider collidedWith)
    {
        if (collidedWith.gameObject == reqObject.gameObject)
        {
            gameObject.SetActive(false);
        }
    }
}
