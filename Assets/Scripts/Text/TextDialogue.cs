using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextDialogue : MonoBehaviour {

    private Text dialogueText;
    private static float DISP = 3f;
    private float displayDelay;

    private bool streamOpen;
    private int streamPos;
    private static float STRM = 3.5f;
    private float streamDelay;
    private string[] stream;

    private Color defaultColor;

    private static bool dialogueEnabled = true;

    // Use this for initialization
    void Start()
    {
        dialogueText = GetComponent<Text>();
        displayDelay = 0f;
        dialogueText.text = "";
        defaultColor = dialogueText.color;

        resetStream();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
            dialogueEnabled = true;
        else if (Input.GetKeyDown(KeyCode.M))
            dialogueEnabled = false;

        if (!dialogueEnabled)
        {
            dialogueText.text = "";
            return;
        }

        if (displayDelay > 0)
        {
            displayDelay = displayDelay - Time.deltaTime;
            if (displayDelay <= 0)
            {
                dialogueText.text = "";
            }
        }
        if (streamOpen)
        {
             if (streamDelay <= 0)
            {
                if (streamPos < stream.Length)
                {
                    OutputStream(stream, streamPos);
                    streamPos++;
                }
                else
                {
                    resetStream();
                }
            }
            else
            {
                streamDelay = streamDelay - Time.deltaTime;
            }
        }
    } 

    public void OutputDialogue(string[] output, int flag)
    {
        resetStream();
        if (flag == 1)
            dialogueText.color = Color.red;
        else if (flag == 2)
            dialogueText.color = Color.green;
        else
            dialogueText.color = defaultColor;
        streamOpen = true;
        stream = output;
    }

    void OutputStream(string[] output, int pos)
    {
        dialogueText.text = output[pos];
        displayDelay = DISP;
        streamDelay = STRM;
    }

    void resetStream()
    {
        streamOpen = false;
        streamPos = 0;
        streamDelay = 0f;
        stream = new string[0];
    }
}
