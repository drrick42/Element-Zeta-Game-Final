using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour
{

    float quitdelay;

    // Use this for initialization
    void Start()
    {
        quitdelay = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        quitdelay = quitdelay - Time.deltaTime;
        if (quitdelay <= 0)
        {
            Application.Quit();

        }
    }
}