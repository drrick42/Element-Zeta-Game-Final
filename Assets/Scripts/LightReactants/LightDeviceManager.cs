using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class LightDeviceManager
{
    public static List<ILightEmitter> pointLights;

    static LightDeviceManager(){
        pointLights = new List<ILightEmitter>();
    }

    public static void RegisterEmitter(ILightEmitter light)
    {
        if(!pointLights.Contains(light))
            pointLights.Add(light);
    }

    //don't see a need for this but it's easy to make
    public static void UnregisterEmitter(ILightEmitter light)
    {
        pointLights.Remove(light);
    }

    public static bool DiscreteLit(GameObject lightSensitiveBlock)
    {
        foreach (var light in pointLights)
        {
            if (light.Emitting && light.CheckHit(lightSensitiveBlock.transform))
                return true;//early terminate
        }
        return false;
    }
}
