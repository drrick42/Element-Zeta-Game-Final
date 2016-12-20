using UnityEngine;
using System.Collections;

public class Doors : MonoBehaviour {

    public bool locked;

    Animator animator;
    bool doorOpen;
    private GameObject enterReq;
    void Start()
    {
        enterReq = GameObject.Find("Player");
        doorOpen = false;
        animator = GetComponent<Animator>();
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (locked) return;
        if (!doorOpen)
        {
            if (col.gameObject == enterReq.gameObject)
            {
                doorOpen = true;
                DoorsControl("Open");
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (doorOpen)
        {
            doorOpen = false;
            DoorsControl("Close");
        }
    }
    void DoorsControl(string direction)
    {
        animator.SetTrigger(direction);
    }
}
