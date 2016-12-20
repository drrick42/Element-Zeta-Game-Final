using UnityEngine;
using System.Collections;

public class HeldObjectBeam : MonoBehaviour {

    //LineRenderer line;
    PlayerMove player;
    private GameObject beamParticles;
    private ParticleSystem particles;

    private GameObject target;

    private float emissions;

	void Start () {
        //line = GetComponent<LineRenderer>();
        beamParticles = Resources.Load<GameObject>("Prefabs/GameObjects/HoldBeam");
        particles = Instantiate(beamParticles).GetComponent<ParticleSystem>();
        player = GetComponent<PlayerMove>();
        emissions = particles.emissionRate;
        target = null;
	}
	
	
	void Update () {
	    if(player.heldObject != null)
        {
            //line.enabled = true;
            //line.SetPosition(0, transform.position);
            //line.SetPosition(1, player.heldObject.transform.position);

            particles.emissionRate = emissions;

            target = player.heldObject;
            
        }
        else
        {
            //line.enabled = false;
            particles.emissionRate = 0;
        }

        if(target != null && particles.particleCount > 0)
        {
            particles.transform.position = transform.position;
            Vector3 dir = target.transform.position - transform.position;
            particles.transform.rotation = Quaternion.LookRotation(dir);
            particles.startLifetime = dir.magnitude / particles.startSpeed;
        }
	}
}
