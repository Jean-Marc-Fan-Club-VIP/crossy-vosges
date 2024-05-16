using Script.GameScript;
using TMPro;
using UnityEngine;

public class HighscoreMenu : MonoBehaviour
{
    private GameStatsController gameStatsController;
    private TMP_Dropdown levelSelector;
    private Transform rowTemplate;
    private Transform table;
    
    
    private void Awake()
    {
        gameStatsController = new GameStatsController();
        table = transform.Find("Table");
        rowTemplate = table.transform.Find("RowTemplate");
        levelSelector = transform.Find("LevelSelect").GetComponent<TMP_Dropdown>();
    }
    
    private void Start()
    {
        DisplayScores(1);
    }
    
    private void DestroyRows()
    {
        for (ushort i = 2; i < table.childCount; i++)
        {
            Destroy(table.GetChild(i)?.gameObject);
        }
    }
    
    private void DisplayScores(int level)
    {
        var bestRuns = gameStatsController.GetBestRuns(level);
        DestroyRows();
        short i = 0;
        foreach (var (playerName, stats) in bestRuns)
        {
            var row = Instantiate(rowTemplate, table);
            row.gameObject.SetActive(true);
            row.Find("Rank").GetComponent<TMP_Text>().SetText((i + 1).ToString());
            row.Find("Score").GetComponent<TMP_Text>().SetText(stats.Score.ToString());
            row.Find("Name").GetComponent<TMP_Text>().SetText(playerName);
            i++;
        }
    }
    
    public void OnLevelChanged()
    {
        DisplayScores(levelSelector.value + 1);
    }
}