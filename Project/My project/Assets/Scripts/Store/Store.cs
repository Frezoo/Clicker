using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    [SerializeField] private Vector3[] CarOnStandPositions;
    [SerializeField] private int[] PricePerIndex;
    private int targetIndex;
    
    [SerializeField] private Transform Cars;
    
    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;
    [SerializeField] private Button buyButton;
    [SerializeField] private TextMeshProUGUI priceText;
    
    [SerializeField] [Range(0,2)] private int currentPosition = 0;

    private bool isMoving = false;

    void Start()
    {
        prevButton.interactable = false;
        priceText.text = PricePerIndex[0] + "$";
        CheckCanWeBuy(0);
    }
    
    public void NextPosition()
    {
        if (isMoving) return;

        if (currentPosition + 1 <= CarOnStandPositions.Length - 1)
        {
            targetIndex = currentPosition + 1;
            priceText.text = PricePerIndex[targetIndex] + "$";
            CheckCanWeBuy(targetIndex);
            StartCoroutine(ChangePosition(targetIndex));
        }
    }

    public void PreviousPosition()
    {
        if (isMoving) return; 

        if (currentPosition - 1 >= 0)
        {
            targetIndex = currentPosition - 1;
            priceText.text = PricePerIndex[targetIndex] + "$";
            CheckCanWeBuy(targetIndex);
            StartCoroutine(ChangePosition(targetIndex));
        }
    }

    private void SetPriceColor(int CurrentPrice)
    {
        if (GameManager.Instance.Score >= CurrentPrice)
        {
            priceText.color = Color.limeGreen;
        }
        else
        {
            priceText.color = Color.red;
        }
        
    }

    private void CheckCanWeBuy(int carIndex)
    {
        SetPriceColor(PricePerIndex[carIndex]);
        if (carIndex > (int)GameManager.Instance.PlayerCarType && GameManager.Instance.Score >= PricePerIndex[carIndex])
        {
            buyButton.interactable = true;
        }
        else
        {
            buyButton.interactable = false;
        }
    }

    public void BuyCar()
    {
        GameManager.Instance.Score -= PricePerIndex[targetIndex];
        GameManager.Instance.PlayerCarType = (GameManager.CarType)targetIndex;
        
        SaveManager.Instance.SaveCarType();
        SaveManager.Instance.SaveScore(GameManager.Instance.Score);
        
        buyButton.interactable = false;
        SetPriceColor(PricePerIndex[targetIndex]);
    }

    public void BackToGame()
    {
        SceneManager.LoadScene(1);
    }
    
    IEnumerator ChangePosition( int newPosIndex)
    {
        isMoving = true;

        Vector3 startPos = Cars.position;
        Vector3 targetPos = CarOnStandPositions[newPosIndex];
        float duration = 0.2f; 
        float elapsed = 0f;

        while (elapsed < duration)
        {
            Cars.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

       
        Cars.position = targetPos;
        currentPosition = newPosIndex;
        
        prevButton.interactable = (currentPosition > 0);
        nextButton.interactable = (currentPosition < CarOnStandPositions.Length - 1);

        isMoving = false;
    }
}