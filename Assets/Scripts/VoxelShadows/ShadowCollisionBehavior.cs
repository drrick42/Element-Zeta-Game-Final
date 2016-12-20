using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cubiquity;

public class ShadowCollisionBehavior : MonoBehaviour
{

    /// <summary>
    /// When an object collides with the shadows, this particle system will be instantiated on the colliding object.
    /// </summary>
    //public GameObject collisionParticles;

    /// <summary>
    /// When an object enters the trigger, its layer gets set to Ignore Raycast.
    /// We need to remember the layer of each object inside so we can restore them.
    /// </summary>
    private Dictionary<GameObject, int> objectLayerMap;

    private static int numObjects = 0;
    private int id;

    private static Material defaultMaterial = null;
    private static Material transparentMaterial = null;

    void Start()
    {
        VoxelShadowBlock v = GetComponent<VoxelShadowBlock>();

        Vector3 scale = new Vector3(v.WidthX, v.WidthY, v.WidthZ);
        scale = scale * v.blockSize;
        scale = new Vector3(scale.x / transform.localScale.x, scale.y / transform.localScale.y, scale.z / transform.localScale.z);

        BoxCollider b = GetComponent<BoxCollider>();
        b.size = scale * 1.1f;
        b.center = scale / 2;
        
        objectLayerMap = new Dictionary<GameObject, int>();

        id = numObjects;
        numObjects++;

        if (defaultMaterial == null)
        {
            defaultMaterial = Resources.Load<Material>("Materials/MoveableObjectOpaque");
            transparentMaterial = Resources.Load<Material>("Materials/MoveableObjectTransparent");
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (IsGrabbableObject(other.transform))
        {
            objectLayerMap.Add(other.gameObject, other.gameObject.layer);
            SetLayer(other.transform, LayerMask.NameToLayer("Ignored By Voxel Shadows"));

            other.GetComponent<MeshRenderer>().material = transparentMaterial;
            //Color c = other.GetComponent<MeshRenderer>().material.color;
            //other.GetComponent<MeshRenderer>().material.color = new Color(c.r, c.g, c.b, 0.5f);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (IsGrabbableObject(other.transform) && objectLayerMap.ContainsKey(other.gameObject))
        {
            SetLayer(other.transform, objectLayerMap[other.gameObject]);
            objectLayerMap.Remove(other.gameObject);

            other.GetComponent<MeshRenderer>().material = defaultMaterial;
            //Color c = other.GetComponent<MeshRenderer>().material.color;
            //other.GetComponent<MeshRenderer>().material.color = new Color(c.r, c.g, c.b, 1);
        }
    }

    void SetLayer(Transform obj, int layer)
    {
        obj.gameObject.layer = layer;
        foreach (Transform t in obj)
            SetLayer(t, layer);
    }

    private bool IsGrabbableObject(Transform t)
    {
        return ItemAttributes.AttOrDefault(t).Grabbable;
    }
}
