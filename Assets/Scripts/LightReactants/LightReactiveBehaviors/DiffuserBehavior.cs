using UnityEngine;
using System.Collections;
using System;

public class DiffuserBehavior : LaserReactiveBlockBehavior, ILightEmitter
{

    float Range = 10000;
	private PrepareSound sound;
    public bool Emitting { get; private set; }

    public Transform Transform
    {
        get
        {
            return transform;
        }
    }

    Transform innerSphere;

    void Start()
    {
		sound = GetComponent<PrepareSound>();
        LightDeviceManager.RegisterEmitter(this);
        innerSphere = transform.FindChild("Sphere").FindChild("LightSphere");
    }

    void Update()
    {
        Emitting = LitByLaser;
        AdjustBehavior(LitByLaser);
    }

    void OnDestroy()
    {
        LightDeviceManager.UnregisterEmitter(this);
    }

    void AdjustBehavior(bool lit)
    {
        GetComponent<Light>().enabled = lit;
        //var contshadow = GetComponent<ShadowController>();
        //if (contshadow != null)
        //    contshadow.Enabled = lit;

		sound.playOnceOrLoop(SoundManager.SoundEffect,lit,true);

        //bloom
        innerSphere.GetComponent<MeshRenderer>().enabled = lit;
    }



    public bool CheckHit(Transform obj)
    {
        RaycastHit info;
        //foreach (Vector3 v in allVecs)
        {
            //Vector3 pos = obj.transform.position;// +v * obj.transform.localScale.x;
            var dir = transform.position - obj.transform.position;
            Physics.Raycast(obj.transform.position, dir, out info, Range);
            if (info.transform == transform)
                return true;
        }
        return false;
    }
}
