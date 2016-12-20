using UnityEngine;
using System.Collections;

public class TimedToggleAlt : MonoBehaviour {

    public float toggleTime;
    private bool entered;

    public GameObject preToggle, postToggle, rejObject;

	// Use this for initialization
	void Start () {
        entered = false;
        preToggle.SetActive(true);
        postToggle.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
        if (entered)
        {
            if (toggleTime > 0) toggleTime = toggleTime - Time.deltaTime;
            if (toggleTime <= 0)
            {
                preToggle.SetActive(false);
                postToggle.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider collidedWith)
    {
        if (collidedWith.gameObject != rejObject.gameObject)
        {
            entered = true;
        }
    }

    void OnTriggerExit(Collider collidedWith)
    {
        if (collidedWith.gameObject != rejObject.gameObject)
        {
            if (toggleTime > 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
