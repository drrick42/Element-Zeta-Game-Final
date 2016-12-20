using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    InputField inField;
    InputField.SubmitEvent se;

    InputManager manager;
    GameObject outputField;
    Text output;

    // Use this for initialization
    void Start()
    {
        manager = gameObject.GetComponentInParent<InputManager>();
        inField = gameObject.GetComponent<InputField>();
        se = new InputField.SubmitEvent();
        se.AddListener(SubmitInput);
        inField.onEndEdit = se;
        outputField = GameObject.Find("Canvas/Console/OutputField/Text");
        output = outputField.GetComponent<Text>();
    }

    private void SubmitInput(string text)
    {
        if (!text.Contains("~"))
        {
            string response = CommandInterpreter.ExecuteCommand(text);
            string currentText = output.text;
            string newText = response;
            //string newText = currentText + "\n" + response;
            output.text = newText;
        }
        inField.text = "";
        PauseManager.UnPauseGame();
        manager.deactivateOut();
        gameObject.SetActive(false);
    }
}