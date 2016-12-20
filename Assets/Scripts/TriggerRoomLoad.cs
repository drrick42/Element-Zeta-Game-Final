using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TriggerRoomLoad : MonoBehaviour
{

    public static Vector3 relativePlayerPos;
    public static bool movePlayerOnStart;

    private GameObject enterReq;
    public string level;
    private bool done;
    private AsyncOperation async;

    private float fadeOutTime = 2;

    private static Texture2D overlay;
    private float fadeAlpha;
    private bool isFading;

    static TriggerRoomLoad()
    {
        movePlayerOnStart = false;
        overlay = null;
    }

    // Use this for initialization
    void Start()
    {
        if(overlay == null)
            overlay = Resources.Load<Texture2D>("Textures/Black");
        isFading = false;
        done = false;
        async = null;
        enterReq = GameObject.Find("Player");
        fadeAlpha = 0;
    }

    IEnumerator LoadLevel()
    {
        done = true;
        //Vector3 relativePlayerPos = transform.position - enterReq.transform.position;
        //movePlayerOnStart = true;

        //AsyncOperation async = Application.LoadLevelAsync(level);
        async = SceneManager.LoadSceneAsync(level);
        async.allowSceneActivation = false;

        isFading = true;
        float timer = 0;
        while (timer < fadeOutTime)
        {
            float r = timer / fadeOutTime;
            fadeAlpha = r;

            timer += Time.deltaTime;
            yield return null;
        }

        PlayerMove.enableMovement = false;
        //while (async.progress < 0.9f)
        //    yield return null;

        //yield return async;

        //for (int i = 0; i < VoxelShadowBlock.InitFrames; i++)
        //{
        //    yield return null;
        //}

        async.allowSceneActivation = true;
    }

    void OnTriggerEnter(Collider collidedWith)
    {
        if (done) return;
        if (collidedWith.gameObject == enterReq.gameObject)
        {
            StartCoroutine("LoadLevel");
        } 
    }

    void OnGUI()
    {
        if (!isFading) return;

        GUI.color = new Color(1, 1, 1, fadeAlpha);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), overlay);
    }

}
