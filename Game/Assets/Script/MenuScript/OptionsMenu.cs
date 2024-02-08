using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsMenuUi;
    public Slider slider;
    public static float volumeSound = 0.5f;

    public static bool GoOptions = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnValueChanged()
    {
        volumeSound = slider.value;
    }

}

