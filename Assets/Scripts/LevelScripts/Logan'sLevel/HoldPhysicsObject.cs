using UnityEngine;
using System.Collections;

public class HoldPhysicsObject : MonoBehaviour {

    private Rigidbody r;
    private int frames;

	void Start () {
        r = GetComponent<Rigidbody>();
        r.isKinematic = true;
        frames = 0;
	}
	
	
	void Update () {
	
        if(frames > VoxelShadowBlock.InitFrames)
        {
            r.isKinematic = false;
            Destroy(this);
        }
        frames++;
	}
}
