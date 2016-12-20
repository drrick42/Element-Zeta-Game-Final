using UnityEngine;
using System.Collections;

public class FloatingPlatform : MonoBehaviour {

    public GameObject start;
    public GameObject end;

    public float speed = 10;

    private GameObject target;

	void Start () {
        transform.position = start.transform.position;
        target = end;
	}
	
	
	void Update () {

        transform.position = Vector3.Lerp(transform.position, target.transform.position, speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, target.transform.position) < 0.1)
        {
            if (target == start) target = end;
            else target = start;
        }
	}
}
