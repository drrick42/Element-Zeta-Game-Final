using UnityEngine;
using System.Collections;

public class EmitterBehavior : MonoBehaviour {

    public bool On { get; set; }

    Transform Laser;
    Vector3 laserScale;

    //laser can only hit one thing at a time
    LaserReactiveBlockBehavior lastLit;

	// Use this for initialization
	void Start () {
        Laser = Instantiate((GameObject)Resources.Load("Prefabs/GameObjects/Laser")).transform;
        Laser.parent = transform;
        Laser.localRotation = Quaternion.FromToRotation(Vector3.forward, Vector3.up);
        laserScale = Laser.localScale;

        On = true;
	}
	
	// Update is called once per frame
    void LateUpdate()
    {
        Laser.GetComponent<Renderer>().enabled = On;
        if (On)
        {
            RaycastHit info;
            Physics.Raycast(transform.position, transform.forward, out info);

            //move beam
            var length = Vector3.Project(info.transform.position - transform.position, transform.forward).magnitude;//to center
            laserScale.y = length / 2;

            Laser.position = transform.position + transform.forward.normalized * length / 2;
            Laser.localScale = laserScale;

            //check if thing hit reacts to light
            LaserReactiveBlockBehavior acc = info.transform.GetComponent<LaserReactiveBlockBehavior>();
            if (acc != null)
            {
                acc.LitByLaser = true;
                if (acc != lastLit)
                {
                    if (lastLit != null)
                        lastLit.LitByLaser = false;
                    lastLit = acc;
                }
            }
            else if (lastLit != null)
            {
                lastLit.LitByLaser = false;
                lastLit = null;
            }
        }
        else
        {
            if (lastLit != null)
            {
                lastLit.LitByLaser = false;
                lastLit = null;
                var s = Laser.localScale;
                s.y = 0;
                Laser.localScale = s;
            }
        }
	}
}
