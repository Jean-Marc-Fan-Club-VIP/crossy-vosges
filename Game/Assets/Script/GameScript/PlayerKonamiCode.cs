using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKonamiCode : MonoBehaviour
{
    public GameObject defaultObject;
    public GameObject konamiObject;

    private bool isKonamiCodeActive = false;

    private List<char> konamiCode = new List<char> { 'U', 'U', 'D', 'D', 'L', 'R', 'L', 'R', 'B', 'A' };
    private List<char> performedAction = new List<char>();

    bool CheckKonamiCode()
    {
        if(performedAction.Count != konamiCode.Count)
        {
            return false;
        }

        for (int i = 0; i < konamiCode.Count; i++)
        {
            if (konamiCode[i] != performedAction[i])
            {
                return false;
            }
        }

        return true;
    }

    void PerformKonamiCode()
    {
        if(!isKonamiCodeActive)
        {
            isKonamiCodeActive = true;

            defaultObject.SetActive(false);
            konamiObject.SetActive(true);
        }
        
    }

    void Update()
    {
        if (Input.anyKeyDown && !isKonamiCodeActive)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                performedAction.Add('U');
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                performedAction.Add('D');
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                performedAction.Add('L');
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                performedAction.Add('R');
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                performedAction.Add('B');
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Q))
            {
                performedAction.Add('A');
            }

            if (performedAction.Count > konamiCode.Count)
            {
                performedAction.RemoveAt(0);
            }

            if (performedAction.Count == konamiCode.Count)
            {
                if (CheckKonamiCode())
                {
                    PerformKonamiCode();
                }
            }
        }
        
    }
}
