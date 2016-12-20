using UnityEngine;
using System.Collections;

public class LightFlashEffect : MonoBehaviour {

    private Light light;

	private bool changeColor; 
	private SoundManager sound = SoundManager.Instance;
	private LightFlashColorChange colorObj;

    public bool IsFlashing { get; private set; }

	private float lerpPoint;
	private float maxIntensity = 8;
	private float minIntensity = 0;
    public float Intensity = 8;
    public float FlashSpeed = 70;
	
	void Start () {
        light = GetComponent<Light>();
        light.intensity = 0;
        IsFlashing = false;
		lerpPoint = 0;

		if(LightFlashColorChange.hasRend(gameObject)){
			changeColor = true;
			colorObj = gameObject.GetComponent<LightFlashColorChange>() as LightFlashColorChange;
		}else { 
			changeColor = false; 
		}
			
		//FlashLight();
	}

    public void FlashLight()
    {
        if (!IsFlashing)
            StartCoroutine("Flash");
        else
        {
			StopCoroutine("Flash");
			StartCoroutine("Flash");
        }
    }

    IEnumerator Flash()
    {
        IsFlashing = true;

		while(light.intensity < Mathf.Min(Intensity, maxIntensity))
        {
			if(changeColor){ colorObj.changeToColor(lerpPoint);}

			light.intensity += FlashSpeed * Time.deltaTime;
			lerpPoint = light.intensity/maxIntensity;

            yield return null;
        }
        light.intensity = Intensity;


		while (light.intensity > minIntensity)
        {
			if(changeColor){ colorObj.changeBackColor(lerpPoint);}
            
			light.intensity -= FlashSpeed * Time.deltaTime;
			lerpPoint = light.intensity/maxIntensity;
            yield return null;
        }

        light.intensity = 0;

		// Uncomment the following it to keep flashing
		//IsFlashing = false;
		//FlashLight();
    }




}
