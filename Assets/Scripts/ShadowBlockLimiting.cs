using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ShadowBlockLimiting : MonoBehaviour
{
    //Attach to player
    private struct arrayToDistance
    {
        public int arrayPos;
        public float distance;
    };

    public GameObject[] shadowBlocks;
    public int maxActiveShadowBlock = 2;//Max number of active shadow blocks.

    private const float MAXDIST = 10000f;

    void Start()
    {
        //No shadowblocks are active at start.
        for (int i = 0; i < shadowBlocks.Length; i++)
        {
            activateBlock(i, false);
        }

        minDistanceFromPlayer();
    }

    public void minDistanceFromPlayer()
    {
        Vector3 playerPos = gameObject.transform.position;
        List<arrayToDistance> minDistance = new List<arrayToDistance>();
        arrayToDistance dist = new arrayToDistance();

        for (int i = 0; i < shadowBlocks.Length; i++)
        {
            activateBlock(i, false);
            Vector3 shadowPos = shadowBlocks[i].transform.position;
            Vector3 distanceFromPlayer = shadowPos - playerPos;

            dist.distance = distanceFromPlayer.magnitude;
            dist.arrayPos = i;

            minDistance.Add(dist);
        }

        minDistance = minDistance.OrderBy(x => x.distance).ToList();

        //Set minimum active blocks
        for (int j = 0; j < maxActiveShadowBlock; j++)
        {
            int pos = minDistance.ElementAt(j).arrayPos;
            activateBlock(pos, true);
            print(shadowBlocks[pos].name);
        }
    }

    private void activateBlock(int pos, bool active)
    {
        shadowBlocks[pos].SetActive(active);
        //shadowBlocks[pos].GetComponent<BoxCollider>().enabled = active;
    }
}

