using UnityEngine;
using System.Collections;

public class L1FirstButton : MonoBehaviour {

    public GameObject lights;

    GetButton button;
    bool pressed;
    bool done;

    float timer;

	void Start () {
        button = GetComponent<GetButton>();
        pressed = false;
        done = false;
        lights.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (done) return;

        if(pressed)
        {
            timer += Time.deltaTime;
            if(timer > 5)
            {
                lights.SetActive(true);
                done = true;
            }
        }

        else if(button.isPressed)
        {
            pressed = true;
            //lights.SetActive(true);
            //GetComponent<RotateOnPedestal>().Rotate(180);
            timer = 0;
        }
	}
}
