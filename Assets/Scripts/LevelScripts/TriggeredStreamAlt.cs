using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class TriggeredStreamAlt : MonoBehaviour {

    public TextAsset enterTxt, exitTxt;
    public bool closeOnEnter, closeOnExit;
    public GameObject enterRej, exitRej;
    private string[] enterOutput, exitOutput;
    private GameObject dialogue;
    private TextDialogue textDialogue;
    public int colorFlag;

    // Use this for initialization
    void Start()
    {
        dialogue = GameObject.Find("Canvas/DialogueText");
        textDialogue = dialogue.GetComponent<TextDialogue>();


    }

    void OnTriggerEnter(Collider collidedWith)
    {
        if (collidedWith.gameObject != enterRej.gameObject)
        {
            enterOutput = Regex.Split(enterTxt.text, "&&");
            textDialogue.OutputDialogue(enterOutput, colorFlag);
            if (closeOnEnter)
            gameObject.SetActive(false);
        }
    }

    void OnTriggerExit(Collider collidedWith)
    {
        if (collidedWith.gameObject != exitRej.gameObject)
        {
            exitOutput = Regex.Split(exitTxt.text, "&&");
            textDialogue.OutputDialogue(exitOutput, colorFlag);
            if (closeOnExit)
            gameObject.SetActive(false);
        }
    }
}
