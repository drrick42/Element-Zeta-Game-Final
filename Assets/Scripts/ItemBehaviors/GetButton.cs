using UnityEngine;
using System.Collections;

public class GetButton : MonoBehaviour
{

    public bool isPressed;

    // Use this for initialization
    void Start()
    {
        isPressed = false;
    }

    public void getPressed(bool buttonPressed)
    {
        isPressed = buttonPressed;
    }
}