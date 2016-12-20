using UnityEngine;
using System.Collections;

public class ObjectReset : MonoBehaviour {

    private Vector3 startPos;

	void Start () {
        startPos = transform.position;
	}
	
	public void ResetObject()
    {
        transform.position = startPos;
    }
}
