using UnityEngine;
using System.Collections;

public abstract class LaserReactiveBlockBehavior : MonoBehaviour {

    public bool LitByLaser { get; set; }

	// Use this for initialization
	void Start () {
        LitByLaser = false;
	}
}
