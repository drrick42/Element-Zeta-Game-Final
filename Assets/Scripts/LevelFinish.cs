using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour {

    private static float increaseParticlesTime = 3;
    private static float maxParticleRate = 500;
    private static float maxParticleLifetime = 2;
    private static float maxParticleSize = 20;
    private static float fadeToBlackTime = 2;

    private ParticleSystem particles;
    private Texture2D overlay;

    private bool fading;
    private float fadeAlpha;

    private static string hubSceneName = "Scenes/World/Puzzles/0-HubRoom";

	void Start ()
    {
        overlay = Resources.Load<Texture2D>("Textures/Black");
        particles = GetComponent<ParticleSystem>();
        fading = false;
        fadeAlpha = 1;
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) StartCoroutine("FadeBackToHub");
    }

    void OnGUI()
    {
        if (!fading) return;

        GUI.color = new Color(1,1,1,fadeAlpha);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), overlay);
    }

    IEnumerator FadeBackToHub()
    {
        float timer = 0;

        float baseRate = particles.emissionRate;
        float baseLife = particles.startLifetime;
        float baseSize = particles.startSize;
        while(timer < increaseParticlesTime)
        {
            float r = timer / increaseParticlesTime;
            float rate = r * maxParticleRate + (1 - r) * baseRate;
            float life = r * maxParticleLifetime + (1 - r) * baseLife;
            float size = r * maxParticleSize + (1 - r) * baseSize;
            particles.emissionRate = rate;
            particles.startLifetime = life;
            particles.startSize = size;

            timer += Time.deltaTime;
            yield return null;
        }

        fading = true;
        timer = 0;
        while(timer < fadeToBlackTime)
        {
            fadeAlpha = timer / fadeToBlackTime;

            timer += Time.deltaTime;
            yield return null;
        }

        SceneManager.LoadScene(hubSceneName);
    }
	
	void OnTriggerEnter(Collider other)
    {
        if(other.name.Equals("Player"))
        {
            PlayerMove.enableMovement = false;
            StartCoroutine("FadeBackToHub");
        }
    }
}
