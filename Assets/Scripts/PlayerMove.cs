using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMove : MonoBehaviour
{
    public static bool enableMovement = true;

    Vector2 absMouse;
    Vector2 smoothMouse;

    public GameObject heldObject;
    Coroutine moveHeldObject;
    int heldObjectLayer;
    float heldObjectMass;
    bool heldObjectWasKinematic;
    CollisionDetectionMode heldObjectCollisionDetectionMode;
    Quaternion desiredRotation;

    GameObject tooltip;
    GameObject tooltipAssembly;
    const float TOOLTIP_OFFSET = 50;
    const float TOOLTIP_MAX_WIDTH = 190;

    Transform cam;

    public Vector2 Sensitivity = new Vector2(2, 2);
    const float SMOOTHX = 3, SMOOTHY = 3;
    const float LOOK_VERT_LIMIT = 170;
    public Vector2 targetDir;
    public Vector2 targetCharDir;

    const float MOVE_SPEED = 20f;
    const float GRAV = 20f;

    const float MAX_INTERACT = 15f;
    const float MAX_LOOK = 50f;
    const float HOVER_DIST = 7f;

    private float camVertRot = 0;

    public bool NoclipEnabled
    {
        get { return noclipEnabled; }
        set { noclipEnabled = value; GetComponent<CharacterController>().enabled = !value; /*GetComponent<BoxCollider>().enabled = !value;*/ }
    }
    private bool noclipEnabled = false;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        cam = transform.FindChild("Main Camera");
        var dir = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
        desiredRotation = Quaternion.Euler(0, 0, 0);

        tooltip = GameObject.Find("Canvas/TooltipAssembly/Tooltip/TooltipText");
        tooltipAssembly = tooltip.transform.parent.parent.gameObject;
        tooltipAssembly.SetActive(false);

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Q)) Application.Quit();

        if (PauseManager.IsPaused) return;
        if (PauseFunctions.IsPaused) return;
        Look();
        Move();
        Interact();
        MoveHeldObject();
    }

    void Look()
    {
        Transform cam = Camera.main.transform;

        var targetOrientation = Quaternion.Euler(targetDir);
        var targetCharacterOrientation = Quaternion.Euler(targetCharDir);

        var mouseDelta = new Vector2(Input.GetAxis("Mouse X") * Sensitivity.x * SMOOTHX,
            Input.GetAxis("Mouse Y") * Sensitivity.y * SMOOTHY);

        // Lerp for smoother look
        smoothMouse.x = Mathf.Lerp(smoothMouse.x, mouseDelta.x, 1f / SMOOTHX);
        smoothMouse.y = Mathf.Lerp(smoothMouse.y, mouseDelta.y, 1f / SMOOTHY);

        absMouse += smoothMouse;

        Quaternion bodyRotation = Quaternion.Euler(0, smoothMouse.x, 0);
        transform.localRotation *= bodyRotation;

        camVertRot -= smoothMouse.y;
        camVertRot = Mathf.Clamp(camVertRot, -90, 90);

        Quaternion cameraRotation = Quaternion.Euler(camVertRot, 0, 0);
        cam.localRotation = cameraRotation;

        //var xRotation = Quaternion.AngleAxis(-absMouse.y, targetOrientation * Vector3.right);
        //transform.localRotation = xRotation;

        //absMouse.y = Mathf.Clamp(absMouse.y, -LOOK_VERT_LIMIT * 0.5f, LOOK_VERT_LIMIT * 0.5f);

        //transform.localRotation *= targetOrientation;

        //var yRotation = Quaternion.AngleAxis(absMouse.x, transform.InverseTransformDirection(Vector3.up));
        //transform.localRotation *= yRotation;

        HandleTooltips();

    }

    void Move()
    {
        if (!enableMovement) return;

        var control = GetComponent<CharacterController>();
        Vector3 moveDir = Vector3.zero;

        moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDir *= MOVE_SPEED * Time.deltaTime;
        //make relative to look 
        Vector3 rot = cam.rotation.eulerAngles;

        if (!NoclipEnabled)
        {
            rot.x = 0; //We're only interested in rotation around y axis
            rot.z = 0;
        }

        moveDir = Quaternion.Euler(rot) * moveDir;

        if (!NoclipEnabled)
        {
            moveDir -= Time.deltaTime * GRAV * Vector3.up;
            control.Move(moveDir);
        }
        else
        {
            transform.position = transform.position + moveDir;
        }

    }

    void Interact()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (heldObject != null)
            {
                UnsetHeldObject();
            }
            else
            {
                if (false)
                {
                    //TODO: in future look for buttons/levers to interact
                }
                else
                {
                    var t = CameraRaycast(MAX_INTERACT);
                    if (t != null && ItemAttributes.AttOrDefault(t).Grabbable)
                    {
                        SetHeldObject(t);
                    }
                }
            }
        }
        if (Input.GetKeyDown("i"))
        {
            RotateHeldObject(0, 90);

        }
        if (Input.GetKeyDown("j"))
        {
            RotateHeldObject(90, 0);

        }
        if (Input.GetKeyDown("k"))
        {
            RotateHeldObject(0, -90);

        }
        if (Input.GetKeyDown("l"))
        {
            RotateHeldObject(-90, 0);


        }
    }

    void RotateHeldObject(float ychange, float zchange)
    {

        desiredRotation = Quaternion.Euler(desiredRotation.eulerAngles.x, desiredRotation.eulerAngles.y + ychange, desiredRotation.eulerAngles.z + zchange);

    }
    void HandleTooltips()
    {
        //find text objects
        Transform hit = CameraRaycast(MAX_LOOK);
        if (hit != null)
        {
            var atts = ItemAttributes.AttOrDefault(hit);
            if (atts.TextApparent)
            {
                tooltipAssembly.SetActive(true);

                tooltip.GetComponent<Text>().text = atts.Text;
                var width = tooltip.GetComponent<Text>().preferredWidth;
                width = Mathf.Min(width, TOOLTIP_MAX_WIDTH);

                var r = tooltip.GetComponent<RectTransform>();
                var r1 = tooltip.transform.parent.GetComponent<RectTransform>();
                var r2 = tooltip.transform.parent.parent.GetComponent<RectTransform>();
                //set width of tooltip
                r.sizeDelta = new Vector2(width, r.sizeDelta.y);

                r.anchoredPosition = new Vector2(-width / 2, r.anchoredPosition.y);
                //set width of tooltip marker
                r1.sizeDelta = new Vector2(width + TOOLTIP_OFFSET, r1.sizeDelta.y);
                //set position of assembly
                r2.position = cam.GetComponent<Camera>().WorldToScreenPoint(hit.transform.position);
            }
            else
            {
                tooltipAssembly.SetActive(false);
            }
        }
        else
        {
            tooltipAssembly.SetActive(false);
        }
    }

    IEnumerator MoveHeldObject()
    {
        var rb = heldObject.GetComponent<Rigidbody>();
        var camPos = transform.FindChild("Main Camera");

        const float MOVE_SPEED = 20;
        const float ROT_SPEED = 20;
        const float ROT_EPSILON = 10f;
        const float MAX_SPEED = 50;

        //if you force the object away don't let it follow
        while ((rb.transform.position - transform.position).sqrMagnitude < MAX_INTERACT * MAX_INTERACT - 5)
        {
            var desiredPos = camPos.position + camPos.forward * HOVER_DIST;
            var vel = MOVE_SPEED * (desiredPos - heldObject.transform.position);
            //vel = new Vector3(Mathf.Clamp(vel.x, -MAX_VELOCITY, MAX_VELOCITY), Mathf.Clamp(vel.y, -MAX_VELOCITY, MAX_VELOCITY), Mathf.Clamp(vel.z, -MAX_VELOCITY, MAX_VELOCITY));
            float mag = vel.magnitude;
            vel.Normalize();
            vel = vel * Mathf.Min(mag, MAX_SPEED);
            rb.velocity = vel;

            var dir = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
            var desiredDir = Quaternion.FromToRotation(Vector3.forward, dir);
            var desiredDir2 = desiredDir * desiredRotation;
            //when we get close, solidify
            if (Quaternion.Angle(desiredDir, heldObject.transform.rotation) > ROT_EPSILON)
            {
                desiredDir = Quaternion.Lerp(heldObject.transform.rotation, desiredDir2, Time.deltaTime * ROT_SPEED);
                rb.MoveRotation(desiredDir);
            }
            else
            {
                rb.MoveRotation(desiredDir2);
                rb.freezeRotation = true;
            }


            yield return null;
        }
        UnsetHeldObject();
    }

    void SetHeldObject(Transform transform)
    {
        heldObject = transform.gameObject;
        heldObject.GetComponent<Rigidbody>().useGravity = false;
        heldObjectLayer = heldObject.layer;
        //SetLayer(transform, LayerMask.NameToLayer("Ignore Raycast"));

        heldObjectMass = heldObject.GetComponent<Rigidbody>().mass;
        heldObject.GetComponent<Rigidbody>().mass = 0;
        heldObjectWasKinematic = heldObject.GetComponent<Rigidbody>().isKinematic;
        heldObject.GetComponent<Rigidbody>().isKinematic = false;

        heldObjectCollisionDetectionMode = heldObject.GetComponent<Rigidbody>().collisionDetectionMode;
        heldObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        moveHeldObject = StartCoroutine(MoveHeldObject());
    }

    void UnsetHeldObject()
    {
        if (moveHeldObject != null)
            StopCoroutine(moveHeldObject);
        var rb = heldObject.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.mass = heldObjectMass;
        rb.isKinematic = heldObjectWasKinematic;
        rb.collisionDetectionMode = heldObjectCollisionDetectionMode;
        //disallow or nerf "throwing" for now TODO: review whether this is intended behavior
        //if throwing is allowed, must turn up drag on grabelements
        rb.velocity *= 0.2f;
        //rb.angularVelocity = Vector3.zero;
        rb.freezeRotation = false;
        //SetLayer(heldObject.transform, heldObjectLayer);

        //Make sound effect play once
        if (heldObject.GetComponent<AddSoundToCollision>() != null)
        {
            heldObject.GetComponent<AddSoundToCollision>().playMusicID = true;
       	}

        heldObject = null;

    }

    Transform CameraRaycast(float dist)
    {
        RaycastHit info;
        Physics.Raycast(cam.position, cam.forward, out info, dist);
        return info.transform;
    }

    void SetLayer(Transform obj, int layer)
    {
        obj.gameObject.layer = layer;
        foreach (Transform t in obj)
            SetLayer(t, layer);
    }

}
