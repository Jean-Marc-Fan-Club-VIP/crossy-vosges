using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Script.GameScript
{
    public class GameStatsController
    {
        private const ushort TableSize = 5;
        private const string StatsPath = "stats.json";
        private static readonly IDataService DataService = new JsonDataService();
        
        
        public List<RunStats> GetGameStats()
        {
            List<RunStats> previousStats;
            try
            {
                previousStats = DataService.LoadEntity<List<RunStats>>(StatsPath);
            }
            catch (Exception e)
            {
                Debug.Log($"{e}. Creating a new stat file");
                previousStats = new List<RunStats>();
            }
            
            return previousStats;
        }
        
        public static void SaveGameStats(List<RunStats> previousStats)
        {
            DataService.SaveEntity(StatsPath, previousStats);
        }
        
        public int GetBestScore(int level)
        {
            var gameStats = GetGameStats().Where(s => s.Level == level).ToList();
            return gameStats.Count > 0 ? gameStats.Max(s => s.Score) : 0;
        }
        
        public RunStats[] GetHighScores(int level)
        {
            return
                (from stat in GetGameStats() orderby stat.Score descending where stat.Level == level select stat)
                .Take(TableSize)
                .ToArray();
        }
    }
}