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
            if (light.color != Color.blue)
            {
                Color newColor;
                ColorUtility.TryParseHtmlString("#BF6AFC", out newColor);
                light.color = newColor;
            }
        }
        isNightMode = true;
    }

    public void DesactivateNightMode()
    {
        // Désactiver le brouillard dans la scène
        RenderSettings.fog = false;
        foreach (Light light in lights)
        {
            if (light.color == Color.blue)
            {
                Color newColor;
                ColorUtility.TryParseHtmlString("#FFDE83", out newColor); 
                light.color = newColor;
            }
        }
        isNightMode = false;
    }

    public static bool IsNightModeActive()
    {
        return isNightMode;
    }
}

