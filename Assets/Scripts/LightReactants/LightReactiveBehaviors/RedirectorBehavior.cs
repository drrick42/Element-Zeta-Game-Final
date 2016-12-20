using UnityEngine;
using System.Collections;

public class RedirectorBehavior : LaserReactiveBlockBehavior {

    float time = 0;

    void Update()
    {
        time += Time.deltaTime * 4;
        gameObject.GetComponent<EmitterBehavior>().On = LitByLaser;
        transform.localRotation = Quaternion.identity;//cool litte wobble
        transform.localPosition = Vector3.zero;
        //transform.localPosition = Vector3.up * Mathf.Sin(time) * 0.2f;

    }
}
