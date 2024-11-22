using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level4 : MonoBehaviour
{
    [SerializeField] private Text timerText;
    [SerializeField] private GameObject winPanel, loosPanel, worms, apple, timer, daleeButton;

    [SerializeField] private AudioClip audioIntro;
    [SerializeField] private AudioClip audioWin;
    [SerializeField] private AudioClip audioLose;

    public bool isGame = false;

    private List<int> numColors = new List<int> {1, 2, 3, 4};
    private float cTime = 15f;
    private bool isTimerActive = false;

    private AudioSource audioSource;


    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioIntro;
    }

    private void Update()
    {
        Timer();
    }

    public void StartEmiter()
    {
        daleeButton.SetActive(true);
    }

    public void ClickStartGame()
    {
        worms.SetActive(true);
        apple.SetActive(true);
        timer.SetActive(true);
        if (winPanel != null) winPanel.SetActive(false);
        if (loosPanel != null) loosPanel.SetActive(false);

        StartCoroutine(PlayAudioAndActivate());  // Запускаем корутину для воспроизведения звука и перехода к следующему кругу
    }

    private IEnumerator PlayAudioAndActivate()  // Ждем ее балабольство, потом начинается игра
    {
        audioSource.Play();

        yield return new WaitForSeconds(audioIntro.length);

        isGame = true;
        isTimerActive = true;
    }

    public void CheckClicked(int value)
    {
        if (isGame)
        {
            if (numColors.Contains(value))
            {
                numColors.Remove(value);
            }

            CheckWin();
        }
    }

    private void CheckWin()
    {
        if (numColors.Count == 0)
        {            
            isGame = false;
            isTimerActive = false;

            winPanel.SetActive(true);
            audioSource.clip = audioWin;
            audioSource.Play();
        }
    }

    private void Timer()
    {
        if (isTimerActive)
        {
            cTime -= Time.deltaTime;

            if (cTime <= 0)
            {
                isGame = false;
                isTimerActive = false;

                loosPanel.SetActive(true);
                audioSource.clip = audioLose;
                audioSource.Play();
            }

            timerText.text = Mathf.Max(0, Mathf.FloorToInt(cTime)).ToString();
        }
    }
}
