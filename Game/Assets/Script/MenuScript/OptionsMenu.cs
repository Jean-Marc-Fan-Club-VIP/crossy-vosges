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
        slider.value = volumeSound;
    }

    void Update()
    {
        slider.value = volumeSound;
    }

    public void OnValueChanged()
    {
        volumeSound = slider.value;
    }

}