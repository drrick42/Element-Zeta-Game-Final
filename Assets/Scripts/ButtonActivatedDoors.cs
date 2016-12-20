using UnityEngine;
using System.Collections;

public class ButtonActivatedDoors : MonoBehaviour {

    Animator animator;
    bool doorOpen;
    private GetButton button;
    private BoxCollider door;

    void Start()
    {
        door = GetComponent<BoxCollider>();
        button = GetComponent<GetButton>();
        doorOpen = false;
        animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        if (button.isPressed)
        {
            if (!doorOpen)
            {
                doorOpen = true;
                DoorsControl("Open");
                door.enabled = false;
            }
        }
        else
        {
            if (doorOpen)
            {
                doorOpen = false;
                DoorsControl("Close");
                door.enabled = true;
            }
        }
    }

    void DoorsControl(string direction)
    {
        animator.SetTrigger(direction);
    }
}
