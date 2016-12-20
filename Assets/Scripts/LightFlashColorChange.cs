using UnityEngine;
using System.Collections;

public class LightFlashColorChange : MonoBehaviour {

	private Renderer rend;
	private Color original;
	private Color flashColor;
	private Material night;

	//Use before starting class
	public static bool hasRend(GameObject gameObj){ return (gameObj.GetComponent<Renderer>()!=null); }

	// Use this for initialization
	public void Start () {
		rend = gameObject.GetComponent<Renderer>();
		rend.material.shader = Shader.Find("Standard");

		original = rend.material.color;
		flashColor = Color.white;
	}

	public void changeToColor(float lerpPoint){
		if(lerpPoint>1.0f){
			lerpPoint = 1.0f;
		}
		rend.material.color = Color.Lerp(original,flashColor,lerpPoint);
	}

	public void changeBackColor(float lerpPoint){
		if(lerpPoint>1.0f){
			lerpPoint = 1.0f;
		}

		lerpPoint = 1-lerpPoint;
		rend.material.color = Color.Lerp(flashColor,original,lerpPoint);
	}


}
