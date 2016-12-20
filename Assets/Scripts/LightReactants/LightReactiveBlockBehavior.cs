using UnityEngine;
using System.Collections;

public abstract class LightReactiveBlockBehavior : MonoBehaviour {

    public bool LitByLight { get; set; }

	// Use this for initialization
	void Start () {
        LitByLight = false;
	}
	
	// Update is called once per frame
	void Update () {
        LitByLight = LightDeviceManager.DiscreteLit(gameObject);
        UpdateLit();
	}

    protected abstract void UpdateLit();
}
