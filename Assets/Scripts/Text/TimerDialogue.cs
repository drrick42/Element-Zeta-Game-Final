using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class TimerDialogue : MonoBehaviour {

    public TextAsset txt;
    public GameObject enterReq, exitReq;
    private string[] output;
    private GameObject dialogue;
    private TextDialogue textDialogue;
    public int colorFlag;

    private bool triggered;
    public float timer;

    // Use this for initialization
    void Start()
    {
        dialogue = GameObject.Find("Canvas/DialogueText");
        textDialogue = dialogue.GetComponent<TextDialogue>();
        triggered = false;

    }

    void Update()
    {
        if (triggered)
        {
            if (timer > 0)
                timer = timer - Time.deltaTime;
            else
            {
                output = Regex.Split(txt.text, "&&");
                textDialogue.OutputDialogue(output, colorFlag);
                gameObject.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider collidedWith)
    {
        if (collidedWith.gameObject == enterReq.gameObject && !triggered)
        {
            triggered = true;
        }
    }

    void OnTriggerExit(Collider collidedWith)
    {
        if (collidedWith.gameObject == exitReq.gameObject && !triggered)
        {
            triggered = true;
        }
    }
}
