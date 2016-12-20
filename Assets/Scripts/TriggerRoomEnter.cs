using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TriggerRoomEnter : MonoBehaviour
{

    private GameObject enterReq;
    public string level;
    // Use this for initialization
    void Start()
    {
        enterReq = GameObject.Find("Player");
    }

    void OnTriggerEnter(Collider collidedWith)
    {
        if (collidedWith.gameObject == enterReq.gameObject)
        {
            SceneManager.LoadScene(level);
        } 
    }

}
