using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightModeManager : MonoBehaviour
{
    public Light[] lights;
    private static bool isNightMode = false;

    private void Update()
    {
        int currentLevel = LevelSelector.LevelGame();
        if (currentLevel == 1)
        {
            Debug.Log("LEVEL 1");
            DesactivateNightMode();
        }
        else if (currentLevel == 2)
        {
            Debug.Log("LEVEL 2");
            ActivateNightMode();
        }
    }

    public void ActivateNightMode()
    {
        // Activer le brouillard dans la scène
        RenderSettings.fog = true;
        foreach (Light light in lights)
        {
            light.intensity *= 0.6f;
        }
        isNightMode = true;
    }

    public void DesactivateNightMode()
    {
        // Désactiver le brouillard dans la scène
        RenderSettings.fog = false;
        foreach (Light light in lights)
        {
            if (light.intensity < 1.25)
            {
                light.intensity /= 0.6f;
            }
        }
        isNightMode = false;
    }

    public static bool IsNightModeActive()
    {
        return isNightMode;
    }
}

