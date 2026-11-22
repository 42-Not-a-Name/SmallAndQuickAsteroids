using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public List<ScoreEntry> scores;

    public GameData()
    {
        this.scores = new List<ScoreEntry>();
    }
}

[System.Serializable]
public struct ScoreEntry
{
    public string name;
    public int score;
}
