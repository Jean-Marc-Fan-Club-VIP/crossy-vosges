using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelector : MonoBehaviour

{
    [SerializeField] private TMP_Dropdown level;

    public void DropdownValueChanged()
    {
        Debug.Log("New Value : " + level.value);
    }
}
