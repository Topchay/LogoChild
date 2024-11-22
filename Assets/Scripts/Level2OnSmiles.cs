using UnityEngine;
using UnityEngine.UI;

public class Level2OnSmiles : MonoBehaviour
{
    [SerializeField] private Level2 Level2;
    [SerializeField] private Image imageComponent;

    [SerializeField] private float countdownTime = 2f;     // Время ожидания
    private float timer = 0f;                              // Таймер для отслеживания времени
    private bool isCountingDown = false;                   // Флаг для отслеживания состояния таймера

    private void Awake()
    {
        Level2 = FindObjectOfType<Level2>();
        imageComponent = GetComponent<Image>();
    }

    private void Update()
    {
        CheckSprite();

        if (isCountingDown)
        {
            timer += Time.deltaTime;

            if (timer >= countdownTime)
            {
                SetImageAlpha(0f);
                ResetCountdown();
            }
        }
    }

    private void CheckSprite()
    {
        if (imageComponent.sprite == Level2.imgBanan)
        {
            if (!isCountingDown)
            {
                isCountingDown = true;
                timer = 0f;
            }
        }
    }

    private void SetImageAlpha(float alpha)
    {
        imageComponent.sprite = null;

        Color color = imageComponent.color;
        color.a = alpha;
        imageComponent.color = color;
    }

    private void ResetCountdown()
    {
        isCountingDown = false;
        timer = 0f;
    }

    private void OnMouseDown()
    {
        if (imageComponent.sprite == Level2.imgBanan)
        {
            //Level2.BananRespawn();
            Level2.pointsBanans++;

            SetImageAlpha(0f);
            ResetCountdown();
        }
    }
}