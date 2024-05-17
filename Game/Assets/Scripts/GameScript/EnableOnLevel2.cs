using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnLevel2 : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);

        if(LevelSelector.LevelGame() > 1)
        {
            gameObject.SetActive(true);
        }
    }
}
