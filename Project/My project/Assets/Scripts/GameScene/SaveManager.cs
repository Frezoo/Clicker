using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    private const string ScoreKey = "Score";
    private const string ClickValueMultiplierKey = "Multiplier";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveScore(int score)
    {
        PlayerPrefs.SetInt(ScoreKey, score);
        PlayerPrefs.Save();
    }
    
    public void SaveMultiplier(int Multiplier)
    {
        PlayerPrefs.SetInt(ClickValueMultiplierKey, Multiplier);
        PlayerPrefs.Save();
    }

    public int LoadScore()
    {
        return PlayerPrefs.GetInt(ScoreKey, 0);
    }
    
    public int LoadMultiplier()
    {
        return PlayerPrefs.GetInt(ClickValueMultiplierKey, 0);
    }
}

