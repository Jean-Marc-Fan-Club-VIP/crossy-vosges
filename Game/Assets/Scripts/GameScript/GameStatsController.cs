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
        
        
        public Dictionary<string, PlayerStats> GetGameStats()
        {
            Dictionary<string, PlayerStats> previousStats;
            try
            {
                previousStats = DataService.LoadEntity<Dictionary<string, PlayerStats>>(StatsPath);
            }
            catch (Exception e)
            {
                Debug.Log($"{e}. Creating a new stat file");
                previousStats = new Dictionary<string, PlayerStats>();
            }
            
            return previousStats;
        }
        
        public static void SaveGameStats(Dictionary<string, PlayerStats> previousStats)
        {
            DataService.SaveEntity(StatsPath, previousStats);
        }
        
        public int GetBestScore(int level)
        {
            return GetAllRuns()
                .Where(run => run.Value.Level == level)
                .Select(run => run.Value.Score)
                .DefaultIfEmpty(0)
                .Max();
        }
        
        public IEnumerable<KeyValuePair<string, RunStats>> GetAllRuns()
        {
            return GetGameStats().SelectMany(player => player.Value.RunsStats
                .Select(runStats => new KeyValuePair<string, RunStats>(player.Key, runStats)));
        }
        
        
        public IEnumerable<KeyValuePair<string, RunStats>> GetBestRuns(int level)
        {
            return GetAllRuns()
                .Where(run => run.Value.Level == level)
                .OrderByDescending(run => run.Value.Score)
                .Take(TableSize);
        }
    }
}