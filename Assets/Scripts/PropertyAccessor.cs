using UnityEngine;
using System.Collections;

public class PropertyAccessor : MonoBehaviour {

    public ItemAttributes Attributes { get; private set; }

    public string ID;

    void Awake()
    {
        Attributes = ItemAttributes.GetAttributes(ID);
    }
}
