using UnityEngine;
using System.Collections;

public class L1BlockLaserWhenRotating : MonoBehaviour {

    public GameObject block;
    private bool wasRotating;
    private RotateOnPedestal rot;

    void Start()
    {
        rot = GetComponent<RotateOnPedestal>();
        block.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (rot.IsRotating && !wasRotating)
            block.SetActive(true);
        else if (!rot.IsRotating && wasRotating)
            block.SetActive(false);

        wasRotating = rot.IsRotating;
    }
}
