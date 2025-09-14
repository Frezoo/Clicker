using System;
using TMPro;
using UnityEngine;

public class UILogic : MonoBehaviour
{
    [SerializeField] private ClickLogic clickLogic;
    private TextMeshProUGUI scoreText;

    void Awake()
    {
       
    }

    void Start()
    {
        GameManager.Instance.UpdateUserInterface += UpdateUI;
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        Debug.Log(SaveManager.Instance.LoadScore());
        scoreText.text = $"Score: {SaveManager.Instance.LoadScore()}";
    }

    void UpdateUI(int score)
    {
        scoreText.text = $"Score: {score}";
    }

    private void OnDisable()
    {
        clickLogic.Clicked -= UpdateUI;
    }
}
