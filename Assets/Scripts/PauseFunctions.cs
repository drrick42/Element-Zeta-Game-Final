using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseFunctions : MonoBehaviour
{

    public static bool IsPaused = false;
    Rect MainMenu = new Rect(Screen.width - Screen.width / 2 - 100, Screen.height - Screen.height / 2 - 90, 200, 180);
    Rect Controls = new Rect(Screen.width - Screen.width / 2 - 200, Screen.height - Screen.height / 2 - 90, 400, 180);
    Rect Credits = new Rect(Screen.width - Screen.width / 2 - 250, Screen.height - Screen.height / 2 - 85, 500, 190);


    private static string hubSceneName = "Scenes/World/Puzzles/0-HubRoom";

    private bool fading;
    private Texture2D overlay;
    private float fadeAlpha;
    private const float fadeToBlackTime = 2;

    private enum Action { ReturnToHub, RestartLevel, QuitGame };
    private Action action;
    public bool credits;

    public bool controls;

    private bool quitGame;

    void Start()
    {
        quitGame = false;
        fading = false;
        controls = false;
        credits = false;
        fadeAlpha = 1;
        overlay = Resources.Load<Texture2D>("Textures/Black");
    }

    // Update is called once per frame
    void Update()
    {

        if (quitGame) Application.Quit();

        if (!PauseManager.IsPaused && Input.GetKeyDown("p"))
        {
            IsPaused = !IsPaused;
            if (controls) controls = !controls;
            if (credits) credits = !credits;
            if (IsPaused)
            {
                Cursor.lockState = CursorLockMode.None;

                UnityEngine.Cursor.visible = true;
                Time.timeScale = 0.01f;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;

                UnityEngine.Cursor.visible = false;
                Time.timeScale = 1;
            }
        }
    }
    void OnGUI()
    {
        if (fading)
        {
            GUI.color = new Color(1, 1, 1, fadeAlpha);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), overlay);
            return;
        }
        if (IsPaused) GUI.Window(0, MainMenu, TheMainMenu, "Pause Menu");

        if (controls) GUI.Window(0, Controls, TheControls, "Controls");

        if (credits) GUI.Window(0, Credits, TheCredits, "Credits");
    }
    void TheControls(int id)
    {
        GUI.skin.button.alignment = TextAnchor.UpperLeft;
        GUILayout.TextArea("\n\tWASD \t\t move \n\tLeft Mouse \t look around\n\tE\t\t Pickup object\n\tIJKL \t\t rotate held object\n");
        if (GUILayout.Button("Back"))
        {
            controls = !controls;
        }
        if (GUILayout.Button("Continue"))
        {
            controls = !controls;

            IsPaused = !IsPaused;
            Cursor.lockState = CursorLockMode.Locked;

            UnityEngine.Cursor.visible = false;
            Time.timeScale = 1;
        }
    }
    void TheCredits(int id)
    {
        GUI.skin.button.alignment = TextAnchor.UpperLeft;
        GUILayout.TextArea("\n\tBen Allen \t\t Level Design and Player Movement \n\tJin Cheah \t\t Sound and level design\n\tDerrick Dent\t Dialog and level design\n\tGwen Stevens \t Hub Room, object rotation and Menues\n\tLogan Wilson \t Shadow and voxel mechanics, level design\n");
        if (GUILayout.Button("Back"))
        {
            credits = !credits;
        }
        if (GUILayout.Button("Continue"))
        {
            credits = !credits;

            IsPaused = !IsPaused;
            Cursor.lockState = CursorLockMode.Locked;

            UnityEngine.Cursor.visible = false;
            Time.timeScale = 1;
        }
    }
    void TheMainMenu(int id)
    {
        if (GUILayout.Button("Continue"))
        {
            IsPaused = !IsPaused;
            Cursor.lockState = CursorLockMode.Locked;

            UnityEngine.Cursor.visible = false;
            Time.timeScale = 1;
        }
        if (GUILayout.Button("Restart Level"))
        {
            Time.timeScale = 1;
            IsPaused = false;
            action = Action.RestartLevel;
            StartCoroutine("FadeToBlack");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (GUILayout.Button("Return To Hub"))
        {
            Time.timeScale = 1;
            IsPaused = false;
            action = Action.ReturnToHub;
            StartCoroutine("FadeToBlack");
            //SceneManager.LoadScene(hubSceneName);
        }
        if (GUILayout.Button("Show Controls"))
        {
            controls = !controls;
        }
        if (GUILayout.Button("Credits"))
        {
            credits = !credits;
        }

        if (GUILayout.Button("Quit"))
        {
            Time.timeScale = 1;
            IsPaused = false;
            action = Action.QuitGame;
            StartCoroutine("FadeToBlack");
            //Application.Quit();
        }
    }

    IEnumerator FadeToBlack()
    {
        float timer = 0;
        
        fading = true;
        while (timer < fadeToBlackTime)
        {
            fadeAlpha = timer / fadeToBlackTime;

            timer += Time.deltaTime;
            yield return null;
        }

        switch (action)
        {
            case Action.ReturnToHub:
                SceneManager.LoadScene(hubSceneName);
                break;
            case Action.RestartLevel:
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case Action.QuitGame:
                quitGame = true;
                break;
        }
    }
}
