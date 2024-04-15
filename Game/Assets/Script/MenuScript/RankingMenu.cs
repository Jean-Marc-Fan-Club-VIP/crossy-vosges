using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RankingMenu : MonoBehaviour
{
    private const string StatsPath = "stats.json";
    private IDataService dataService;


    private void Start()
    {
        dataService = new JsonDataService();
        var gameStats = dataService.LoadEntity<IEnumerable<GameStats>>(StatsPath);
        var test = (from stat in gameStats orderby stat.Score select stat).Take(5);
    }
}