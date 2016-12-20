using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ObjectState
{
    Vector3 position;
    Quaternion rotation;

    Vector3 linVel;
    Vector3 rotVel;

    MonoBehaviour[] scripts;

    public ObjectState(GameObject obj)
    {
        position = obj.transform.position;
        rotation = obj.transform.rotation;

        var rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            linVel = rb.velocity;
            rotVel = rb.angularVelocity;
        }
    }

    public void Restore(GameObject obj)
    {
        obj.transform.position = position;
        obj.transform.rotation = rotation;

        var rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = linVel;
            rb.angularVelocity = rotVel;
        }
    }

    
}

