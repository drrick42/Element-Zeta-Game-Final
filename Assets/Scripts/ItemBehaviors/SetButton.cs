using UnityEngine;
using System.Collections;

public class SetButton : MonoBehaviour {

    public GameObject responder;
    private GetButton receiver;
    private MeshRenderer clicker;
    private bool isButtonPressed;

    // Use this for initialization
    void Start () {
        isButtonPressed = false;
        clicker = gameObject.GetComponent<MeshRenderer>();
        receiver = responder.GetComponent<GetButton>();
	}

    void OnTriggerEnter(Collider collidedWith)
    {
                isButtonPressed = true;
                receiver.getPressed(isButtonPressed);
                clicker.enabled = false;
    }

    void OnTriggerExit(Collider collidedWith)
    {
                isButtonPressed = false;
                receiver.getPressed(isButtonPressed);
                clicker.enabled = true;
    }
}
