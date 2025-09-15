using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    private const string ScoreKey = "Score";
    private const string ClickValueMultiplierKey = "Multiplier";
    private const string CarTypeKey = "CarType";
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

    public void SaveCarType()
    {
        PlayerPrefs.SetInt(CarTypeKey, (int)GameManager.Instance.PlayerCarType);
        PlayerPrefs.Save();
    }
    
    public int LoadCarType()
    {
        return PlayerPrefs.GetInt(CarTypeKey, 0);
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

