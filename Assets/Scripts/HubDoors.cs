using UnityEngine;
using System.Collections;

public class HubDoors : MonoBehaviour {

    private static readonly int NUM_LEVELS = 9;

    public Doors[] doors;
    public EmitterBehavior[] emitters;

    private static bool[] locked;
    private static bool[] completed;

    private static bool updateDoors;
    private static bool loaded;

    static HubDoors()
    {
        updateDoors = true;
        loaded = false;
        locked = new bool[NUM_LEVELS];
        completed = new bool[NUM_LEVELS];
    }

    public static void LockDoor(int index)
    {
        if (index < 0 || index >= NUM_LEVELS) return;

        locked[index] = true;
        updateDoors = true;
    }

    public static void UnlockDoor(int index)
    {
        if (index < 0 || index >= NUM_LEVELS) return;

        locked[index] = false;
        updateDoors = true;
    }

    public static void MarkCompleted(int index)
    {
        if (index < 0 || index >= NUM_LEVELS) return;

        completed[index] = true;
        updateDoors = true;
    }

    void Start ()
    {
        if (!loaded)
        {
            LoadGameData();
            loaded = true;
        }
	}

    void OnDestroy()
    {
        SaveGameData();
    }

    public static void SaveGameData()
    {
        int[] data = new int[NUM_LEVELS * 2];

        for(int i = 0; i < NUM_LEVELS; i++)
        {
            if (locked[i]) data[i] = 1;
            else data[i] = 0;
        }

        for(int i = 0; i < NUM_LEVELS; i++)
        {
            if (completed[i]) data[i + NUM_LEVELS] = 1;
            else data[i + NUM_LEVELS] = 0;
        }

        SaveLoadManager.SaveGameData(data);
    }
	
    public static void LoadGameData()
    {
        int[] data = SaveLoadManager.LoadGameData();

        if(data == null)
        {
            locked[1] = false;
            for (int i = 1; i < NUM_LEVELS; i++) locked[i] = true;
            for (int i = 0; i < NUM_LEVELS; i++) completed[i] = false;
            return;
        }

        for(int i = 0; i < NUM_LEVELS; i++)
        {
            if (data[i] == 0) locked[i] = false;
            else locked[i] = true;
        }
        for(int i = 0; i < NUM_LEVELS; i++)
        {
            if (data[i + NUM_LEVELS] == 0) completed[i] = false;
            else completed[i] = true;
        }
    }
	
	void Update ()
    {
	    if(updateDoors)
        {
            for(int i = 0; i < NUM_LEVELS; i++)
            {
                if (locked[i])
                {
                    doors[i].locked = true;
                }
                else
                {
                    doors[i].locked = false;
                }
            }
        }
	}
}
