using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public static float volumeSound = 0.5f;

    public static bool GoOptions = false;
    public GameObject optionsMenuUi;
    public Slider slider;
    public TMP_InputField playerNameInput;

    private void Start()
    {
        slider.value = volumeSound;
        playerNameInput.text = GameStats.CurrentPlayerName;
    }

    private void Update()
    {
        slider.value = volumeSound;
    }

    public void OnPlayerNameChanged()
    {
        GameStats.CurrentPlayerName = playerNameInput.text;
    }

    public void OnValueChanged()
    {
        volumeSound = slider.value;
    }
}