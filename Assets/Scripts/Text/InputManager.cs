using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    GameObject input;
    GameObject output;
    InputField inField;
    float delay;
    bool deactivate;

    // Use this for initialization
    void Start () {
        input = GameObject.Find("Canvas/Console/InputField");
        output = GameObject.Find("Canvas/Console/OutputField");
        inField = input.GetComponent<InputField>();
        delay = 0;
        deactivate = false;
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            if (input.activeInHierarchy && output.activeInHierarchy)
            {
                PauseManager.UnPauseGame();
                input.SetActive(false);
                output.SetActive(false);
            }
            else
            {
                PauseManager.PauseGame();
                input.SetActive(true);
                output.SetActive(true);
                inField = input.GetComponent<InputField>();
                inField.ActivateInputField();
            }
        }

        if (delay >= 0)
        {
            delay = delay - Time.deltaTime;
        }
        else
        {
            if (deactivate)
            {
                output.SetActive(false);
                deactivate = false;
            }
        }
    }

    public void deactivateOut()
    {
        delay = 1f;
        deactivate = true;
    }
}
