using UnityEngine;
using System.Collections;

public class RotatePedestalOnButton : MonoBehaviour {

    public float rotationAngle = 90;

    public bool onDown = true;

    GetButton button;
    RotateOnPedestal rotate;

    bool wasPressed;

	void Start () {
        button = GetComponent<GetButton>();
        rotate = GetComponent<RotateOnPedestal>();
        wasPressed = false;
	}
	
	
	void Update () {
        if (button.isPressed && (!wasPressed || !onDown))
            rotate.Rotate(rotationAngle);

        wasPressed = button.isPressed;
	}
}
