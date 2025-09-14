using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClickVisual : MonoBehaviour
{
    [Header("Настройки покачивания")] 
    public float wiggleAmplitude = 15f;
    public float wiggleSpeed = 10f;


    private float targetAngle;
    private float targetScale = 1.1f;

    private Vector3 originalScale = new Vector3(.85f, .85f, .85f);

    private bool isMovingRight = true;
    
    [SerializeField] private Sprite[] CarsImages;
    [SerializeField] private Image ButtonImage;

    void Start()
    {
        ButtonImage = GetComponent<Image>();
        ButtonImage.sprite = CarsImages[(int)GameManager.Instance.PlayerCarType];
        
    }

    void Update()
    {
        MakeButtonWobbling();
    }

    public void ButtonClick()
    {
        StopAllCoroutines();
        StartCoroutine(ScaleUpAndRestore());
    }

    private void MakeButtonWobbling()
    {
        float currentAngle = transform.eulerAngles.z;
        float angleDifference = targetAngle - currentAngle;


        angleDifference = NormalizeAngle(angleDifference);


        float moveAmount = wiggleSpeed * Time.deltaTime;
        if (Mathf.Abs(angleDifference) < moveAmount)
        {
            isMovingRight = !isMovingRight;
            targetAngle = isMovingRight ? wiggleAmplitude : -wiggleAmplitude;
        }
        else
        {
            transform.Rotate(0, 0, angleDifference > 0 ? moveAmount : -moveAmount);
        }
    }

    private IEnumerator ScaleUpAndRestore()
    {
        float upDuration = 0.05f;
        float downDuration = 0.15f;
        Vector3 startScale = originalScale;
        Vector3 peakScale = new Vector3(targetScale, targetScale, targetScale);

        
        float elapsed = 0f;
        while (elapsed < upDuration)
        {
            float t = elapsed / upDuration;
            float scale = Mathf.Lerp(startScale.x, peakScale.x, t);
            transform.localScale = new Vector3(scale, scale, scale);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = peakScale;

       
        elapsed = 0f;
        while (elapsed < downDuration)
        {
            float t = elapsed / downDuration;
            float scale = Mathf.SmoothStep(peakScale.x, startScale.x, t);
            transform.localScale = new Vector3(scale, scale, scale);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = startScale;
    }

    float NormalizeAngle(float angle)
    {
        while (angle > 180f) angle -= 360f;
        while (angle < -180f) angle += 360f;
        return angle;
    }
}