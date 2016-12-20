using UnityEngine;
using System.Collections;

public class TeleportPlayerTrigger : MonoBehaviour {

    public GameObject teleportLocation;

    private static float raycastLength = 1.3f;

    private GameObject player;
    private Vector3 prevGroundedPosition;
    private LayerMask layerMask;

    void Start()
    {
        player = GameObject.Find("Player");
        layerMask = ~((1 << LayerMask.NameToLayer("Ignored By Voxel Shadows")) | (1 << LayerMask.NameToLayer("Ignore Raycast")));
    }
    

    void Update()
    {
        RaycastHit info;
        bool grounded = Physics.Raycast(player.transform.position, Vector3.down, out info, raycastLength, layerMask);
        
        if(grounded)
        {
            prevGroundedPosition = player.transform.position;
        }
    }

	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            player.transform.position = prevGroundedPosition;
        }
        else
        {
            ObjectReset obj = other.GetComponent<ObjectReset>();
            if (obj != null) obj.ResetObject();
        }
    }
}
