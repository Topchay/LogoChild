using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level7 : MonoBehaviour
{
    [SerializeField] private Text messageText;
    [SerializeField] private Text timerText;

    [SerializeField] private GameObject winPanel, loosPanel, ldinkiGame, daleeButton, zimnyyPlaneta;

    [SerializeField] private AudioClip audioIntro;
    [SerializeField] private AudioClip audioWin;
    [SerializeField] private AudioClip audioLose;
    private AudioSource audioSource;

    private int currentNumber = 1;
    private bool isGameActive = false;
    private float timeRemaining = 3f;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioIntro;
    }

    public void StartEmiter()
    {
        daleeButton.SetActive(true);
        zimnyyPlaneta.SetActive(true);
    }

    public void ClickStartGame()
    {
        ldinkiGame.SetActive(true);
        if (winPanel != null) winPanel.SetActive(false);
        if (loosPanel != null) loosPanel.SetActive(false);

        StartCoroutine(PlayAudioAndActivate());
    }

    private IEnumerator PlayAudioAndActivate()  // Сначала проигрываем её балабольство, потом начинается игра
    {
        audioSource.Play();

        yield return new WaitForSeconds(audioIntro.length);

        audioSource.clip = null;

        isGameActive = true;
        messageText.text = "Льдинка " + currentNumber;
        StartCoroutine(Timer());
    }

    public void CheckClicked(int value)
    {
        if (!isGameActive) return;
        if (value == currentNumber)
        {
            currentNumber++;
            if (currentNumber > 10)
            {
                isGameActive = false;
                winPanel.SetActive(true);
                audioSource.clip = audioWin;
                audioSource.Play();
            }
            else
            {
                messageText.text = "Льдинка " + currentNumber;
                timeRemaining = 3f;
            }
        }
    }

    private IEnumerator Timer()
    {
        while (timeRemaining > 0)
        {
            timerText.text = "Осталось времени: " + Mathf.Ceil(timeRemaining);
            timeRemaining -= Time.deltaTime;
            yield return null;
        }

        messageText.text = "Время вышло! Игра окончена.";
        isGameActive = false;
        loosPanel.SetActive(true);
        audioSource.clip = audioLose;
        audioSource.Play();
    }
}
