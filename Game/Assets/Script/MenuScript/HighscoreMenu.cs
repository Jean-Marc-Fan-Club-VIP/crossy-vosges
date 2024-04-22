using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

public class HighscoreMenu : MonoBehaviour
{
    private const string StatsPath = "stats.json";
    private const ushort TableSize = 5;
    private IDataService dataService;
    private Transform rowTemplate;
    private Transform table;


    private void Awake()
    {
        dataService = new JsonDataService();
        table = transform.Find("Table");
        rowTemplate = table.transform.Find("RowTemplate");
    }

    private void Start()
    {
        try
        {
            var gameStats = dataService.LoadEntity<IEnumerable<RunStats>>(StatsPath);
            var highscores = (from stat in gameStats orderby stat.Score descending select stat).Take(TableSize)
                .ToArray();
            for (ushort i = 0; i < highscores.Length; i++)
            {
                var row = Instantiate(rowTemplate, table);
                row.gameObject.SetActive(true);
                row.Find("Rank").GetComponent<TMP_Text>().SetText((i + 1).ToString());
                row.Find("Score").GetComponent<TMP_Text>().SetText(highscores[i].Score.ToString());
                row.Find("Name").GetComponent<TMP_Text>().SetText(highscores[i].Name);
            }
        }
        catch (FileNotFoundException)
        {
            Debug.Log("Highscores file does not exist. Creating it");
            RunStats[] emptyStats = { };
            dataService.SaveEntity(StatsPath, emptyStats);
        }
    }
}