using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Level2 : MonoBehaviour
{
    private UIManager UIManager;

    [SerializeField] private GameObject[] slotsBanans;
    [SerializeField] private GameObject winPanel, loosPanel, daleeButton, planetaSlonov;
    [SerializeField] private Text timerText;
    [SerializeField] private float cTime = 30;
    [SerializeField] private float respawnInterval = 2f; // Интервал между спавном бананов
    [SerializeField] private bool isActive = true;

    [SerializeField] private AudioClip audioIntro;
    [SerializeField] private AudioClip audioWin;
    [SerializeField] private AudioClip audioLose;
    private AudioSource audioSource;
    private bool isTimerActive = false;
    private bool isGameActive = false;

    public Sprite imgBanan;
    public int pointsBanans = 0;

    private void Awake()
    {
        winPanel.SetActive(false);
        loosPanel.SetActive(false);
        UIManager = FindObjectOfType<UIManager>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioIntro;

    }

    public void StartEmiter()
    {
        daleeButton.SetActive(true);
        planetaSlonov.SetActive(true);
    }

    public void StartGame()
    {
        StartCoroutine(PlayAudioAndActivate());
    }

    private void Update()
    {
        Timer();
        CheckWin();
        CheckLose();
    }

    private IEnumerator PlayAudioAndActivate()
    {
        audioSource.Play();

        yield return new WaitForSeconds(audioIntro.length);

        isTimerActive = true;
        isGameActive = true;
        StartCoroutine(BananRespawnCoroutine());
    }

    private IEnumerator BananRespawnCoroutine()
    {
        while (isActive)
        {
            BananRespawn();
            yield return new WaitForSeconds(respawnInterval); // Ждем 3 секунды перед следующим спавном
        }
    }

    public void BananRespawn()
    {
        int randomIndex = Random.Range(0, slotsBanans.Length);
        GameObject chosenSlot = slotsBanans[randomIndex];
        Image imageComponent = chosenSlot.GetComponent<Image>();

        imageComponent.sprite = imgBanan;

        Color currentColor = imageComponent.color;
        currentColor.a = 1f; // Делаем спрайт видимым
        imageComponent.color = currentColor;
    }

    private void Timer()
    {
        if (isTimerActive)
        {
            cTime -= Time.deltaTime;
            timerText.text = Mathf.Max(0, Mathf.FloorToInt(cTime)).ToString();
        }
    }

    private void CheckWin()
    {
        if (pointsBanans >= 10)
        {
            Win();
        }
    }

    private void CheckLose()
    {
        if (cTime <= 0)
        {
            Lose();
        }
    }

    private void Win()
    {
        isGameActive = false;
        isTimerActive = false;
        winPanel.SetActive(true);
        audioSource.clip = audioWin;
        audioSource.Play();
    }

    private void Lose()
    {
        isGameActive = false;
        isTimerActive = false;
        loosPanel.SetActive(true);
        audioSource.clip = audioLose;
        audioSource.Play();
    }
}