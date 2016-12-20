using UnityEngine;
using System.Collections;

public class DelayedActivation : MonoBehaviour {

    public int delay;
    public bool isGrav;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        isGrav = rb.useGravity;
        rb.useGravity = false;
        delay = VoxelShadowBlock.InitFrames;
	}
	
	// Update is called once per frame
	void Update () {
	    if (delay <= 0f)
        {
            if (!rb.useGravity)
            {
                rb.useGravity = true;
            }
        }
        else
        {
            delay--;
        }
	}
}
