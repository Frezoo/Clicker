using System;
using UnityEngine;

public class ClickLogic : MonoBehaviour
{
    
    

    public Action<int> Clicked;
    
    private int clickValue;


    public void ClickHandler()
    {
        switch (GameManager.Instance.PlayerCarType)
        {
            case GameManager.CarType.Standart:
                clickValue = 10;
                break;
            case GameManager.CarType.Sport:
                clickValue = 20;
                break;
            case GameManager.CarType.Luxury:
                clickValue = 30;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        Clicked?.Invoke(clickValue);
    }

    
}