using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MemoryManager : MonoBehaviour {

    GameObject[] memorableObjects;
    ObjectState[,] states;
    ObjectState[] beginStates;
    
    const int MAX_MEM = 1000;

    int tracker = 0;
    int Tracker
    {
        get { return tracker; }
        set { tracker = value % MAX_MEM; while (tracker < 0) tracker += MAX_MEM; }
    }


    public void Start()
    {
        var all = new List<PropertyAccessor>(GameObject.FindObjectsOfType<PropertyAccessor>());
        List<GameObject> objs = new List<GameObject>();
        for (int i = 0; i < all.Count; i++)
        {
            if (all[i].Attributes == null)
                Debug.Log("Attributes for " + all[i].gameObject.name + " not loaded");
            if (all[i].Attributes.Memorable)
            {
                objs.Add(all[i].gameObject);
            }
        }
        memorableObjects = objs.ToArray();
        states = new ObjectState[all.Count, MAX_MEM];
        beginStates = new ObjectState[memorableObjects.Length];
        for (int i = 0; i < beginStates.Length; i++)
            beginStates[i] = new ObjectState(memorableObjects[i]);
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    void Record()
    {
        //increment tracker, record state
        Tracker++;
        for (int i = 0; i < memorableObjects.Length; i++)
        {
            states[i, tracker] = new ObjectState(memorableObjects[i]);
        }
    }

    void Rewind()
    {
        //recall state, forget, decrement tracker
        if (states.Length > 0 && states[0, tracker] != null)
        {
            for (int i = 0; i < memorableObjects.Length; i++)
            {
                states[i, tracker].Restore(memorableObjects[i]);
                states[i, tracker] = null;
            }
            Tracker--;
        }
    }

    void Reset()
    {
        for (int i = 0; i < memorableObjects.Length; i++)
        {
            beginStates[i].Restore(memorableObjects[i]);
        }
    }

}
