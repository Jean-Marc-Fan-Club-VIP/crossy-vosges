using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public static string PlayerName = "Player";
    public static float volumeSound = 0.5f;

    public static bool GoOptions = false;
    public Slider slider;
    private TMP_InputField nameInput;

    private void Awake()
    {
        nameInput = transform.Find("PlayerNameInput").GetComponent<TMP_InputField>();
    }

    private void Start()
    {
        slider.value = volumeSound;
        nameInput.text = PlayerName;
    }

    private void Update()
    {
        slider.value = volumeSound;
    }

    public void OnValueChanged()
    {
        volumeSound = slider.value;
    }

    public void OnPlayerNameChanged()
    {
        PlayerName = nameInput.text;
    }
}