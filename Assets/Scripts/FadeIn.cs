using UnityEngine;
using System.Collections;

public class FadeIn : MonoBehaviour {

    private float fadeInTime = 2;

    private Texture2D overlay;
    private float fadeAlpha;

    void Awake()
    {
        SoundManager.Instance.ClearSounds();
    }

	void Start ()
    {
        overlay = Resources.Load<Texture2D>("Textures/Black");
        fadeAlpha = 1;
        StartCoroutine("BeginFadeIn");
    }

    IEnumerator BeginFadeIn()
    {
        for(int i = 0; i < VoxelShadowBlock.InitFrames; i++)
        {
            yield return null;
        }
        PlayerMove.enableMovement = true;

        float timer = 0;
        while(timer < fadeInTime)
        {
            float r = timer / fadeInTime;
            fadeAlpha = 1-r;

            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(this);
    }
	
	void OnGUI ()
    {
        GUI.color = new Color(1, 1, 1, fadeAlpha);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), overlay);
    }
}
