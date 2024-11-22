using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level5 : MonoBehaviour
{
    [SerializeField] private Text timerText;
    [SerializeField] private Sprite[] trueFalse;
    [SerializeField] private GameObject winPanel, loosPanel, frukty, levelBar, daleeButton;

    [SerializeField] private AudioClip audioIntro;
    [SerializeField] private AudioClip audioWin;
    [SerializeField] private AudioClip audioLose;

    [SerializeField] private AudioClip[] audioFrukty;
    private AudioSource audioSource;

    [SerializeField] private int currentNumber = 0; // Текущий номер объекта. Начинаем с 1 для первого клика
    private bool isGameActive = false;              // Флаг, указывающий, активно ли текущее игровое состояние

    private float cTime;                            // Время обратного отсчета
    private const float startTime = 4f;             // Начальное время 3 секунды
    private bool isTimerActive;                     // Активация таймера


    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioIntro;
    }

    public void StartEmiter()
    {
        daleeButton.SetActive(true);
    }

    public void ClickStartGame()
    {
        levelBar.SetActive(true);
        frukty.SetActive(true);
        if (winPanel != null) winPanel.SetActive(false);
        if (loosPanel != null) loosPanel.SetActive(false);

        StartCoroutine(PlayAudioAndActivate());  // Запускаем корутину для воспроизведения звука и перехода к следующему кругу
    }

    private void Update()
    {
        Timer();
    }

    private void Timer()
    {
        if (isTimerActive)
        {
            cTime -= Time.deltaTime;

            if (cTime <= 0)
            {
                Image currentImageComponent = levelBar.transform.GetChild(currentNumber).GetComponent<Image>();
                NextFaceFalse(currentImageComponent);
            }

            timerText.text = Mathf.Max(0, Mathf.FloorToInt(cTime)).ToString();
        }
    }

    private IEnumerator PlayAudioAndActivate()  // Ждем ее балабольство, потом начинается игра
    {
        audioSource.Play();

        yield return new WaitForSeconds(audioIntro.length);

        StartNextRound();
        isTimerActive = true;
    }

    private void StartNextRound()
    {
        if (currentNumber < 6)
        {
            cTime = startTime;
            audioSource.clip = audioFrukty[currentNumber];
            audioSource.Play();
            isGameActive = true;
        }
        else
        {
            CheckWin();  // Вызываем метод для обработки победы
        }
    }

    private void NextFaceTrue(Image face)       // Метод для обработки правильного ответа
    {
        currentNumber++;
        face.sprite = trueFalse[1];             // Устанавливаем спрайт для правильного ответа
        StartNextRound();                       // Переходим к следующему раунду
    }

    private void NextFaceFalse(Image face)      // Метод для обработки неправильного ответа
    {
        currentNumber++;
        face.sprite = trueFalse[0];                   // Устанавливаем спрайт для неправильного ответа
        StartNextRound();                             // Переходим к следующему раунду
    }

    public void CheckClicked(int value)
    {
        if (!isGameActive) return;

        Image currentImageComponent = levelBar.transform.GetChild(currentNumber).GetComponent<Image>(); // Получаем компонент изображения для текущего объекта уровня

        if (value == currentNumber) // Если значение совпадает с текущим номером
        {
            NextFaceTrue(currentImageComponent); // Переходим к методу для правильного ответа
        }
        else // Если значение не совпадает
        {
            NextFaceFalse(currentImageComponent); // Переходим к методу для неправильного ответа
        }
    }

    private void CheckWin()
    {
        isGameActive = false; // Деактивируем состояние игры
        isTimerActive = false;
        bool looseSlot = false;

        for (int i = 0; i < levelBar.transform.childCount; i++)
        {
            Image currentImageComponent = levelBar.transform.GetChild(i).GetComponent<Image>();
            if (currentImageComponent.sprite != trueFalse[1]) looseSlot = true;
        }

        if (looseSlot)
        {
            loosPanel.SetActive(true);
            audioSource.clip = audioLose;
            audioSource.Play();
        }
        else
        {
            winPanel.SetActive(true);
            audioSource.clip = audioWin;
            audioSource.Play();
        }
    }
}
