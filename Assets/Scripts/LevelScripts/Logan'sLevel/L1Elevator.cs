using UnityEngine;
using System.Collections;

public class L1Elevator : MonoBehaviour {

    public float height = 50;
    public float moveSpeed = 10;
    public float stationTime = 4;

    private float startHeight;
    private float waitTime;
    private bool up;

	void Start () {
        startHeight = transform.position.y;
        waitTime = -1;
        up = true;
	}
	
	void Update () {
	
        if(waitTime < 0)
        {
            float move = moveSpeed * Time.deltaTime;
            if (!up) move *= -1;

            transform.Translate(0, move, 0);

            if(up)
            {
                if (transform.position.y >= height)
                {
                    transform.Translate(0, transform.position.y - height, 0);
                    waitTime = 0;
                }
            }
            else
            {
                if (transform.position.y <= startHeight)
                {
                    transform.Translate(0, -transform.position.y + startHeight, 0);
                    waitTime = 0;
                }
            }
        }
        else
        {
            waitTime += Time.deltaTime;
            if (waitTime > stationTime)
            {
                waitTime = -1;
                up = !up;
            }
        }

	}
}
