using System;

public class GameStats
{
    public static string CurrentPlayerName = "Default";

    public string PlayerName { get; set; } = CurrentPlayerName;
    public TimeSpan Time { get; set; }
    public int Score { get; set; }
}