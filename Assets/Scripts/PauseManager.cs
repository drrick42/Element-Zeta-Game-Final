using UnityEngine;
using System.Collections;

public static class PauseManager {

    public static bool IsPaused { get; private set; }
    private static float previousTimeScale;

	public static void PauseGame()
    {
        if(!IsPaused)
        {
            IsPaused = true;
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }
    }

    public static void UnPauseGame()
    {
        if(IsPaused)
        {
            IsPaused = false;
            Time.timeScale = previousTimeScale;
        }
    }

}
