using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score
{
    public void SaveScore(int score)
    {
        if (!PlayerPrefs.HasKey("Score")) PlayerPrefs.SetInt("Score", 0);
        else if (PlayerPrefs.GetInt("Score") < score) PlayerPrefs.SetInt("Score", score);
    }
    public int GetScore()
    {
        if (!PlayerPrefs.HasKey("Score")) PlayerPrefs.SetInt("Score", 0);
        return PlayerPrefs.GetInt("Score");
    }
}
