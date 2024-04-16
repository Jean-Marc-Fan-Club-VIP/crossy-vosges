using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown level;

    private static TMP_Dropdown staticDropdown;

    private void Start()
    {
        level.value = 0;

    }

    private void Awake()
    {
        staticDropdown = level;
    }

    public void DropdownValueChanged()
    {
        Debug.Log("Level : " + (level.value + 1));
    }

    public static int LevelGame()
    {
        return staticDropdown.value + 1;
        
    }
}
