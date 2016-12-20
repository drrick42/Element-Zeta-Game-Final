using UnityEngine;
using System.Collections;

public class BarrierBehavior : MonoBehaviour {

    public bool initialOn = true;
    private float enableSpeed = 30;

    public bool BarrierOn {
        get
        {
            return barrierOn;
        }
        set
        {
            barrierOn = value;
            StopAllCoroutines();
            if (barrierOn)
            {
                StartCoroutine("TurnOn");
            }
            else
            {
                StartCoroutine("TurnOff");
            }
        }
    }
    private bool barrierOn;

    private ParticleSystem particles;

    private float height;
    private float emissionRate;

	void Start () {
        particles = GetComponentInChildren<ParticleSystem>();

        var shape = particles.shape;
        shape.box = transform.localScale;

        float volume = transform.localScale.x * transform.localScale.y;
        emissionRate = (int)(volume / 10);
        particles.emissionRate = emissionRate;

        height = transform.localScale.y;

        barrierOn = initialOn;
        if (!initialOn)
        {
            transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
            particles.emissionRate = 0;
        }
    }



    IEnumerator TurnOn()
    {
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;

        while (transform.localScale.y < height)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + enableSpeed*Time.deltaTime, transform.localScale.z);
            yield return null;
        }

        transform.localScale = new Vector3(transform.localScale.x, height, transform.localScale.z);
        particles.emissionRate = emissionRate;
    }

    IEnumerator TurnOff()
    {
        particles.emissionRate = 0;

        while (transform.localScale.y >= 0)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - enableSpeed*Time.deltaTime, transform.localScale.z);
            yield return null;
        }

        transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
    }

}
