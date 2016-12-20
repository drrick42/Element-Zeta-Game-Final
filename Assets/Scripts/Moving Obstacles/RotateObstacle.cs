using UnityEngine;
using System.Collections;

public class RotateObstacle : MonoBehaviour {

    public Vector3 rotationSpeed = new Vector3(0, 10, 0);
	
	void Start () {
	
	}
	
	
	void Update () {

        Quaternion rot = Quaternion.Euler(rotationSpeed * Time.deltaTime);
        transform.rotation = transform.rotation * rot;
	}
}
