using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level3 : MonoBehaviour
{
    private UIManager UIManager;

    [SerializeField] private Text timerText;
    [SerializeField] private GameObject winPanel, loosPanel, smiles, timer, daleeButton, levelBar;

    [SerializeField] private Sprite[] trueFalse;

    [SerializeField] private AudioClip audioIntro;
    [SerializeField] private AudioClip audioWin;
    [SerializeField] private AudioClip audioLose;
    private AudioSource audioSource;

    public Sprite[] smilesSprite;

    private bool isTimerActive = false;
    public bool isGameActive = false;
    private float cTime = 15f;
    [SerializeField] private int currentNumber = 0;


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
        smiles.SetActive(true);
        timer.SetActive(true);
        levelBar.SetActive(true);
        if (winPanel != null) winPanel.SetActive(false);
        if (loosPanel != null) loosPanel.SetActive(false);

        StartCoroutine(PlayAudioAndActivate());
    }

    private void Update()
    {
        Timer();
        Check();
    }

    private IEnumerator PlayAudioAndActivate()
    {
        audioSource.Play();

        yield return new WaitForSeconds(audioIntro.length);

        isTimerActive = true;
        isGameActive = true;
    }

    public void CheckClicked(Sprite image)
    {
        if (!isGameActive) return;

        Image currentImageComponent = levelBar.transform.GetChild(currentNumber).GetComponent<Image>();

        if (image == smilesSprite[0]) 
        {
            currentImageComponent.sprite = trueFalse[1];
        }
        else
        {
            currentImageComponent.sprite = trueFalse[0];
        }
        currentNumber++;
    }

    private void Timer()
    {
        if (isTimerActive)
        {
            cTime -= Time.deltaTime;

            if (cTime <= 0)
            {
                Lose();
            }

            timerText.text = Mathf.Max(0, Mathf.FloorToInt(cTime)).ToString();
        }
    }

    private void Check()
    {
        if (currentNumber >= 7)
        {
            isTimerActive = false;
            isGameActive = false;

            bool allDone = true;
            int length = levelBar.transform.childCount;

            for (int i = 0; i < length; i++)
            {
                Image imageSlot = levelBar.transform.GetChild(i).GetComponent<Image>();
                if (imageSlot.sprite == trueFalse[0]) allDone = false;
            }

            if (!allDone)
            {
                Lose();
            }
            else Win();
        }
    }

    private void Win()
    {
        winPanel.SetActive(true);
        audioSource.clip = audioWin;
        audioSource.Play();
    }

    private void Lose()
    {
        loosPanel.SetActive(true);
        audioSource.clip = audioLose;
        audioSource.Play();
    }
}
