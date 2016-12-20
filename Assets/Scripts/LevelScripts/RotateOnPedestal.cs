using UnityEngine;
using System.Collections;

public class RotateOnPedestal : MonoBehaviour {

    private float rotationAngle;
    private bool isRotating;
    private float rotSpeed = 50;

    public bool IsRotating { get { return isRotating; } }

    public bool Rotate(float angle)
    {
        if (isRotating) return false;

        rotationAngle = angle;
        StartCoroutine("Rot");
        return true;
    }

    IEnumerator Rot()
    {
        isRotating = true;
        float rot = 0;

        while (rot < Mathf.Abs(rotationAngle))
        {
            float r = rotSpeed * Time.deltaTime;
            if (rotationAngle < 0) r = -r;
            rot += Mathf.Abs(r);
            if(rot > Mathf.Abs(rotationAngle))
            {
                if (r > 0)
                    r -= rot - rotationAngle;
                else
                    r += rot + rotationAngle;
            }
            
            transform.localRotation = Quaternion.Euler(transform.up * r) * transform.localRotation;
            yield return null;
        }

        isRotating = false;
    }
}
