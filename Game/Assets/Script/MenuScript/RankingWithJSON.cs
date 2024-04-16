using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class RankingWithJSON : MonoBehaviour
{
    [SerializeField] private TMP_Text textExample;

    string chemin, jsonString;

    void Start()
    {
        /*chemin = Application.streamingAssetsPath + "/datasOldPlayers.json" ;
        jsonString = File.ReadAllText(chemin);
        OldPlayer player = JsonUtility.FromJson<OldPlayer>(jsonString);
        Debug.Log(jsonString);
        Debug.Log(player);
        textExample.text = player.Name + " ; " + player.Level + " ; " + player.Score;*/
        /* // Charger le contenu du fichier JSON en tant que texte
         chemin = Application.streamingAssetsPath + "/datasOldPlayers.json";
         jsonString = File.ReadAllText(chemin);

         // Utiliser JsonUtility pour désérialiser le JSON en un objet C#
         OldPlayer[] oldPlayers = JsonHelper.FromJson<OldPlayer>(jsonString);

         // Accéder aux données lues
         foreach (OldPlayer p in oldPlayers)
         {
             Debug.Log($"Name: {p.Name}, Level: {p.Level}, Score: {p.Score}");
         }*/
    }


    void Update()
    {

    }
}

public class OldPlayer
{
    public string Name;
    public int Level;
    public int Score;
    public OldPlayer(string name, int level, int score) { Name = name; Level = level; Score = score; }
}