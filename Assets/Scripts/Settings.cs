using UnityEngine;
using System.Collections;

public static class Settings {

    public const int LIGHTSPEED_ADVANCE = 100;
    public const int LIGHTSPEED_RETREAT = -1000;

    public const bool RENDER_MATERIALIZE = true;
    public const float MATERIALIZE_SPEED = 1;

    public static readonly Material DarkSolidMat, LightSolidMat, DarkSolidTransparent, LightSolidTransparent;

    static Settings(){
        DarkSolidMat = Resources.Load<Material>("Materials/Darksolid Voxel");
        LightSolidMat = Resources.Load<Material>("Materials/Lightsolid Voxel");
        DarkSolidTransparent = Resources.Load<Material>("Materials/DarkSolid Transparent");
        LightSolidTransparent = Resources.Load<Material>("Materials/LightSolid Transparent");
    }
}
    

