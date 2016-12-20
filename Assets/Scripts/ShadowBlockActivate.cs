using UnityEngine;
using System.Collections;

public class ShadowBlockActivate : MonoBehaviour
{

    private bool triggered = false;
    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.name == "Player")
        {
            triggered = true;
            other.GetComponent<ShadowBlockLimiting>().minDistanceFromPlayer();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (triggered)
        {
            triggered = false;
        }
    }
}
