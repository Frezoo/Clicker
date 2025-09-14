using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public  Action<int> UpdateUserInterface;
    
    private ClickLogic clickLogic;

    
    private int score;
    
    public enum CarType 
    {
        Standart,
        Sport,
        Luxury
    }
    
    public CarType PlayerCarType = CarType.Standart;


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
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }

    private void Start()
    {
        
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(scene.buildIndex);
        if (scene.buildIndex == 1)
        {
            clickLogic = GameObject.Find("GameSceneManager").GetComponent<ClickLogic>();
            
            clickLogic.Clicked += UpdateScore;
            score = SaveManager.Instance != null ? SaveManager.Instance.LoadScore() : 0;
            
            Debug.Log(clickLogic);
        }
    }

    private void UpdateScore(int clickValue)
    {
        Debug.Log($"Score updated by {clickValue}");
        score += clickValue;
        SaveManager.Instance.SaveScore(score);

        Debug.Log("Update");
        UpdateUserInterface?.Invoke(score);
    }
    
    private void OnDisable()
    {
        if (clickLogic != null)
        {
            clickLogic.Clicked -= UpdateScore;
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
